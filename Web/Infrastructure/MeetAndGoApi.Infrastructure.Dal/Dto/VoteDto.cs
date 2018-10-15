using System;
using AutoMapper.Configuration.Conventions;
using MeetAndGo.Shared.Enums;
using MeetAndGo.Shared.Models;

namespace MeetAndGoApi.Infrastructure.Dal.Dto
{
    public class VoteDto
    {
        public Guid VoteDtoId { get; set; }

        public UserDto UserDto { get; set; }
        public Guid UserDtoId { get; set; }

        [MapTo(nameof(VoteModel.VoteTarget))]
        public UserDto TargetDto { get; set; }
        public Guid VoteTargetId { get; set; }

        public UserStatus RatingType { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
    }
}
