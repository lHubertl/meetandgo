using MeetAndGo.Shared.Enums;

namespace MeetAndGo.Shared.Models
{
    public class VoteModel
    {
        public MemberModel VoteTarget { get; set; }
        public UserStatus RatingType { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
    }
}
