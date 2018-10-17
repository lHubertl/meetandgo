using System;
using AutoMapper.Configuration.Conventions;
using MeetAndGo.Shared.Models;

namespace MeetAndGoApi.Infrastructure.Dal.Dto
{
    public class CommentDto
    {
        public Guid CommentDtoId { get; set; }

        [MapTo(nameof(CommentModel.Author))]
        public ApplicationUser ApplicationUser { get; set; }
        public Guid EventDtoId { get; set; }

        public EventDto EventDto { get; set; }
        public string ApplicationUserId { get; set; }
        
        public DateTimeOffset CommentedIn { get; set; }
        public string Text { get; set; }
    }
}
