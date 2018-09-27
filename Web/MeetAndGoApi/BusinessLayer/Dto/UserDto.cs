using System;
using MeetAndGo.Shared.Models;

namespace MeetAndGoApi.BusinessLayer.Dto
{
    public class UserDto : UserModel, IDtoBase
    {
        public Guid Id { get; set; }
    }
}
