﻿using System.Collections.Generic;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Models;
using MeetAndGoApi.Infrastructure.Dto;
using System.Threading.Tasks;

namespace MeetAndGoApi.Infrastructure.Contracts.Service
{
    public interface IUserService
    {
        Task<IResponseData<ApplicationUser>> GetAppUserAsync(string userId);
        Task<IResponseData<UserModel>> GetUserModelAsync(string userId);
        Task<IResponse> SetUserPhotoAsync(string userId, string name, string path);
        Task<IResponseData<List<PointModel>>> GetUserLatestPointsAsync(string userId);
    }
}
