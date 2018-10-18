using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Extensions;
using MeetAndGo.Shared.Managers;
using MeetAndGo.Shared.Models.Authorization;
using MeetAndGoApi.Infrastructure.Dal.Dto;
using MeetAndGoApi.Infrastructure.Resources;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public AccountController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IStringLocalizer<Strings> localizer,
            ILogger<AccountController> logger,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _localizer = localizer;
            _configuration = configuration;
        }

        #region Api implementation

        /// <summary>
        /// Check if the user has not registered yet. Otherwise send SMS with a confirmation
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IResponse> ConfirmPhoneNumber(MessageConfirmModel model)
        {
            var validator = ValidationManager.Create()
                .Validate(() => model != null, _localizer.GetString(Strings.V_ParameterCanNotBeNull))
                .ValidatePhoneNumber(model?.PhoneNumber, _localizer.GetString(Strings.V_PhoneNumber));

            if (!validator.IsValid)
            {
                return new Response(ResponseCode.RequestError, validator.ToString());
            }

            var phoneNumber = model.PhoneNumber.CleanPhoneNumber();

            var result = await _userManager.Users.AnyAsync(user => user.PhoneNumber == phoneNumber);
            if (result)
            {
                return new Response(ResponseCode.RequestError, _localizer.GetString(Strings.V_UserExist));
            }

            //
            // TODO: implemet checking user and sending sms
            //
            throw new NotImplementedException();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IResponse> ConfirmMessageCode(MessageConfirmModel model)
        {
            var validator = ValidationManager.Create()
                .Validate(() => model != null, _localizer.GetString(Strings.V_ParameterCanNotBeNull))
                .ValidateSmsCode(model?.Code, _localizer.GetString(Strings.V_Code))
                .ValidatePhoneNumber(model?.PhoneNumber, _localizer.GetString(Strings.V_PhoneNumber));

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
            throw new NotImplementedException();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IResponseData<string>> Register(RegisterModel model)
        {
            var validator = ValidationManager.Create()
                .Validate(() => model != null, _localizer.GetString(Strings.V_ParameterCanNotBeNull))
                .Validate(() => model?.FirstName.Length > 0, _localizer.GetString(Strings.V_FirstName))
                .Validate(() => model?.LastName.Length > 0, _localizer.GetString(Strings.V_LastName))
                .ValidatePhoneNumber(model?.PhoneNumber, _localizer.GetString(Strings.V_PhoneNumber))
                .ValidatePassword(model?.Password, _localizer.GetString(Strings.V_Password))
                .Validate(() => model?.Password == model.ConfirmPassword,
                    _localizer.GetString(Strings.V_DifferentPasswords));

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
                var tokenResult = GenerateJwtToken(model.PhoneNumber, user);

                return new ResponseData<string>(tokenResult.ToString(), ResponseCode.Ok);
            }

            return new ResponseData<string>(ResponseCode.RequestError, result.Errors.FirstOrDefault()?.Description);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IResponseData<string>> SignIn(LoginModel model)
        {
            var validator = ValidationManager.Create()
                .Validate(() => model != null, _localizer.GetString(Strings.V_ParameterCanNotBeNull))
                .ValidatePhoneNumber(model?.PhoneNumber, _localizer.GetString(Strings.V_PhoneNumber))
                .ValidatePassword(model?.Password, _localizer.GetString(Strings.V_Password));

            if (!validator.IsValid)
            {
                return new ResponseData<string>(ResponseCode.RequestError, validator.ToString());
            }

            var phoneNumber = model.PhoneNumber.CleanPhoneNumber();

            var result = await _signInManager.PasswordSignInAsync(phoneNumber, model.Password, model.RememberMe, true);
            if (result.Succeeded)
            {
                var tokenResult = GenerateJwtToken(model.PhoneNumber, new ApplicationUser { UserName = phoneNumber});
                return new ResponseData<string>(tokenResult.ToString(), ResponseCode.Ok);
            }
            
            return new ResponseData<string>(ResponseCode.RequestError, _localizer.GetString(Strings.V_SignIn));
        }

        #endregion

        #region Private methods

        private object GenerateJwtToken(string email, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
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