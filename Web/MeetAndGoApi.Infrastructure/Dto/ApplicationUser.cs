using System;
using System.Collections.Generic;
using MeetAndGo.Shared.Enums;
using Microsoft.AspNetCore.Identity;

namespace MeetAndGoApi.Infrastructure.Dto
{
    public class ApplicationUser : IdentityUser
    {
        public List<EventUser> EventUsers { get; set; }
        public List<CommentDto> CommentDtos { get; set; }
        public List<VoteDto> VoteDtos { get; set; }

        public FileDto FileDto { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double OrganizerRating { get; set; }
        public double MemberRating { get; set; }
        public string CompressedPhotoUrl { get; set; }
        public string HighQualityPhoto { get; set; }
        public string LanguageCode { get; set; }
        public UserStatus Status { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
