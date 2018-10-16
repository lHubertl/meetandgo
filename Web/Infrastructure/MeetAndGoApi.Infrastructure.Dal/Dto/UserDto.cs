using System;
using System.Collections.Generic;
using AutoMapper.Configuration.Conventions;
using MeetAndGo.Shared.Enums;
using MeetAndGo.Shared.Models;

namespace MeetAndGoApi.Infrastructure.Dal.Dto
{
    public class UserDto
    {
        [MapTo(nameof(UserModel.Id))]
        public Guid UserDtoId { get; set; }
        
        public List<EventUser> EventUsers { get; set; }
        public List<CommentDto> CommentDtos { get; set; }

        [MapTo(nameof(UserModel.Votes))]
        public List<VoteDto> VoteDtos { get; set; }
        
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public double OrganizerRating { get; set; }
        public double MemberRating { get; set; }
        public string CompressedPhotoUrl { get; set; }
        public string HighQualityPhoto { get; set; }
        public string LanguageCode { get; set; }
        public UserStatus Status { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
