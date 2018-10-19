using System;
using MeetAndGo.Shared.Enums;

namespace MeetAndGoApi.Infrastructure.Dto
{
    public class VoteDto
    {
        public Guid VoteDtoId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }

        public ApplicationUser TargetDto { get; set; }
        public Guid VoteTargetId { get; set; }

        public UserStatus RatingType { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
    }
}
