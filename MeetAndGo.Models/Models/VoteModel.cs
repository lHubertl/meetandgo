using System;
using MeetAndGo.Shared.Enums;

namespace MeetAndGo.Shared.Models
{
    public class VoteModel
    {
        public Guid VoteModelId { get; set; }
        public MemberModel Voter { get; set; }
        public MemberModel Target { get; set; }
        public UserStatus RatingType { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
    }
}
