using System;
using System.Collections.Generic;
using MeetAndGo.Shared.Enums;

namespace MeetAndGo.Shared.Models
{
    public class UserModel : MemberModel
    {
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public UserStatus Status { get; set; }
        public string LanguageCode { get; set; }
        public List<VoteModel> Votes { get; set; }
    }
}
