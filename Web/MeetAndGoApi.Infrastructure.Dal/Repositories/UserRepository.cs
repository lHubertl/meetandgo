using System.Threading.Tasks;
using AutoMapper;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Models;
using MeetAndGoApi.Infrastructure.Contracts.Repository;
using MeetAndGoApi.Infrastructure.Dto;
using MeetAndGoApi.Infrastructure.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace MeetAndGoApi.Infrastructure.Dal.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IStringLocalizer<Strings> _localizer;
        private readonly IMapper _mapper;
        private readonly Context.DbContext _dbContext;

        public UserRepository(
            Context.DbContext dbContext,
            IMapper mapper,
            IStringLocalizer<Strings> localizer)
        {
            _dbContext = dbContext;
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task<IResponseData<ApplicationUser>> GetAppUserAsync(string id)
        {
            // TODO: check it
            var userResult = await _dbContext.Users.FindAsync(id);

            if (userResult is null)
            {
                return new ResponseData<ApplicationUser>(ResponseCode.RequestError, _localizer.GetString(Strings.UserNotFound));
            }

            return new ResponseData<ApplicationUser>(userResult);
        }

        public async Task<IResponseData<UserModel>> GetUserModelAsync(string id)
        {
            var userResult = await _dbContext.Users.Include(user => user.FileDto).FirstOrDefaultAsync(user => user.Id == id);

            if (userResult is null)
            {
                return new ResponseData<UserModel>(ResponseCode.RequestError, _localizer.GetString(Strings.UserNotFound));
            }

            var userModel = _mapper.Map<UserModel>(userResult);
            return new ResponseData<UserModel>(userModel);
        }

        public async Task<IResponse> SetUserPhotoAsync(string userId, string name, string path)
        {
            var userResult = await _dbContext.Users.FindAsync(userId);
            var fileDto = new FileDto
            {
                ApplicationUser = userResult,
                Name = name,
                Path = path
            };

            var existingFile = await _dbContext.Files.FirstOrDefaultAsync(file => file.ApplicationUserId == userId);
            if (existingFile != null)
            {
                existingFile.Name = name;
                existingFile.Path = path;

                _dbContext.Files.Update(existingFile);
            }
            else
            {
                await _dbContext.Files.AddAsync(fileDto);
            }

            await _dbContext.SaveChangesAsync();

            return new Response();
        }
    }
}
