using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Enums;
using MeetAndGo.Shared.Models;
using MeetAndGo.Shared.Models.Authorization;

namespace MeetAndGoMobile.Services.FakeServices
{
    class FakeAccountService : IAccountService
    {
        public async Task<IResponse> ConfirmPhoneNumberAsync(MessageConfirmModel model, CancellationToken token)
        {
            return new Response(ResponseCode.Ok);
        }

        public async Task<IResponse> ConfirmSmsCodeAsync(MessageConfirmModel model, CancellationToken token)
        {
            return new Response(ResponseCode.Ok);
        }

        public async Task<IResponseData<UserModel>> GetUserModelAsync(CancellationToken token)
        {
            var userModel = new UserModel
            {
                CompressedPhotoUrl = @"https://cdn1.iconfinder.com/data/icons/hawcons/32/698838-icon-111-search-128.png",
                FirstName = "Valerii Sovytskyi",
                LastName = null,
                Votes = new List<VoteModel>
                {
                    new VoteModel { Rating = 2, RatingType = UserStatus.Member },
                    new VoteModel { Rating = 3, RatingType = UserStatus.Member },
                    new VoteModel { Rating = 5, RatingType = UserStatus.Member }
                },
                OrganizerRating = 4,
                MemberRating = 2
            };

            return new ResponseData<UserModel>(userModel);
        }

        public async Task<IResponseData<string>> RegisterAsync(RegisterModel model, CancellationToken token)
        {
            return new ResponseData<string>("1234");
        }

        public async Task<IResponseData<string>> SignInAsync(LoginModel model, CancellationToken token)
        {
            return new ResponseData<string>("1234");
        }
    }
}
