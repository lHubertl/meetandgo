using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Extensions;
using MeetAndGo.Shared.Managers;
using MeetAndGo.Shared.Models;
using MeetAndGo.Shared.Models.Authorization;
using MeetAndGoApi.Extensions;
using MeetAndGoApi.Infrastructure.Contracts.Service;
using MeetAndGoApi.Infrastructure.Dto;
using MeetAndGoApi.Infrastructure.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace MeetAndGoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer<Strings> _localizer;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public AccountController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IStringLocalizer<Strings> localizer,
            IHostingEnvironment appEnvironment,
            ILogger<AccountController> logger,
            IConfiguration configuration,
            IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _localizer = localizer;
            _configuration = configuration;
            _userService = userService;
            _appEnvironment = appEnvironment;
        }

        #region Api implementation

        /// <summary>
        /// Check if the user has not registered yet. Otherwise send SMS with a confirmation
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IResponse> ConfirmPhoneNumber(MessageConfirmModel model)
        {
            if (model is null)
            {
                return new Response(ResponseCode.RequestError, _localizer.GetString(Strings.ParameterCanNotBeNull));
            }

            var validator = ValidationManager.Create()
                .ValidatePhoneNumber(model.PhoneNumber, _localizer.GetString(Strings.PhoneNumber));

            if (!validator.IsValid)
            {
                return new Response(ResponseCode.RequestError, validator.ToString());
            }

            var phoneNumber = model.PhoneNumber.CleanPhoneNumber();

            var result = await _userManager.Users.AnyAsync(user => user.PhoneNumber == phoneNumber);
            if (result)
            {
                return new Response(ResponseCode.RequestError, _localizer.GetString(Strings.UserExist));
            }

            //
            // TODO: implement checking user and sending sms
            //
            return new Response(ResponseCode.Ok);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IResponse> ConfirmMessageCode(MessageConfirmModel model)
        {
            if (model is null)
            {
                return new Response(ResponseCode.RequestError, _localizer.GetString(Strings.ParameterCanNotBeNull));
            }

            var validator = ValidationManager.Create()
                .ValidateSmsCode(model.Code, _localizer.GetString(Strings.Code))
                .ValidatePhoneNumber(model.PhoneNumber, _localizer.GetString(Strings.PhoneNumber));

            if (!validator.IsValid)
            {
                return new Response(ResponseCode.RequestError, validator.ToString());
            }

            var phoneNumber = model.PhoneNumber.CleanPhoneNumber();
            var result = await _userManager.Users.AnyAsync(user => user.PhoneNumber == phoneNumber);
            if (result)
            {

            }

            // TODO: implement checking sms code

            return new Response(ResponseCode.Ok);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IResponseData<string>> Register(RegisterModel model)
        {
            if (model is null)
            {
                return new ResponseData<string>(ResponseCode.RequestError, _localizer.GetString(Strings.ParameterCanNotBeNull));
            }

            var validator = ValidationManager.Create()
                .Validate(() => model.FirstName?.Length > 0, _localizer.GetString(Strings.FirstName))
                .ValidatePhoneNumber(model.PhoneNumber, _localizer.GetString(Strings.PhoneNumber))
                .ValidatePassword(model.Password, _localizer.GetString(Strings.Password))
                .Validate(() => model.Password == model.ConfirmPassword,
                    _localizer.GetString(Strings.DifferentPasswords));

            if (!validator.IsValid)
            {
                return new ResponseData<string>(ResponseCode.RequestError, validator.ToString());
            }

            var phoneNumber = model.PhoneNumber.CleanPhoneNumber();
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = phoneNumber,
                PhoneNumber = phoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, model.RememberMe);
                var userAccount = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == phoneNumber);
                var tokenResult = GenerateJwtToken(userAccount?.Id);

                return new ResponseData<string>(tokenResult.ToString());
            }

            return new ResponseData<string>(ResponseCode.RequestError, result.Errors.FirstOrDefault()?.Description);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IResponseData<string>> SignIn(LoginModel model)
        {
            if (model is null)
            {
                return new ResponseData<string>(ResponseCode.RequestError, _localizer.GetString(Strings.ParameterCanNotBeNull));
            }

            var validator = ValidationManager.Create()
                .ValidatePhoneNumber(model.PhoneNumber, _localizer.GetString(Strings.PhoneNumber))
                .ValidatePassword(model.Password, _localizer.GetString(Strings.Password));

            if (!validator.IsValid)
            {
                return new ResponseData<string>(ResponseCode.RequestError, validator.ToString());
            }

            var phoneNumber = model.PhoneNumber.CleanPhoneNumber();

            var result = await _signInManager.PasswordSignInAsync(phoneNumber, model.Password, model.RememberMe, true);
            if (result.Succeeded)
            {
                var userAccount = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == phoneNumber);
                var tokenResult = GenerateJwtToken(userAccount?.Id);
                return new ResponseData<string>(tokenResult.ToString());
            }
            
            return new ResponseData<string>(ResponseCode.RequestError, _localizer.GetString(Strings.SignIn));
        }

        [HttpGet]
        [Authorize]
        public async Task<IResponseData<UserModel>> GetUserModel()
        {
            var userId = User.CurrentUserId();
            var userResponse = await _userService.GetUserModelAsync(userId);

            // This is for changing domain
            var userModel = userResponse.Data;
            if (userModel != null)
            {
                userModel.CompressedPhotoUrl = $"{Request.Scheme}://{Request.Host}{userModel.CompressedPhotoUrl}";
            }

            userResponse.Data = userModel;

            return userResponse;
        }

        [HttpPost]
        [Authorize]
        public async Task<IResponse> UpdateUserModel(UserModel model)
        {
            if (model is null)
            {
                return new ResponseData<string>(ResponseCode.RequestError, _localizer.GetString(Strings.ParameterCanNotBeNull));
            }

            var validator = ValidationManager.Create()
                .Validate(() => model.FirstName?.Length > 0, _localizer.GetString(Strings.FirstName))
                .ValidateEmail(model.Email, _localizer.GetString(Strings.InvalidEmailFormat));

            if (!validator.IsValid)
            {
                return new ResponseData<string>(ResponseCode.RequestError, validator.ToString());
            }
            

            var currentUser = await _userManager.GetUserAsync(User);
            currentUser.FirstName = model.FirstName;
            currentUser.LastName = model.LastName;
            currentUser.Email = model.Email;
            currentUser.DateOfBirth = model.DateOfBirth;

            var result = await _userManager.UpdateAsync(currentUser);

            if (!result.Succeeded)
            {
                return new Response(ResponseCode.RequestError, result.ToString());
            }

            return new Response();
        }

        [HttpPost]
        [Authorize]
        public async Task<IResponse> UploadProfilePhoto(IFormFile file)
        {
            if (file is null)
            {
                return new Response(ResponseCode.ParameterIsNull, _localizer.GetString(Strings.ParameterCanNotBeNull));
            }

            var userId = User.CurrentUserId();
            
            var path = $"{_appEnvironment.ContentRootPath}\\Files\\Images\\{file.FileName}";
            var webPath = $"/StaticFiles/Images/{file.FileName}";

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return await _userService.SetUserPhotoAsync(userId, file.FileName, webPath);
        }

        #endregion

        #region Private methods

        private object GenerateJwtToken(string userId)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userId)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion
    }
}