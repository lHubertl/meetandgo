using System;
using AutoMapper.Configuration.Conventions;
using MeetAndGo.Shared.Models;

namespace MeetAndGoApi.Infrastructure.Dal.Dto
{
    public class CommentDto
    {
        public Guid CommentDtoId { get; set; }

        [MapTo(nameof(CommentModel.Author))]
        public UserDto UserDto { get; set; }
        public Guid EventDtoId { get; set; }

        public EventDto EventDto { get; set; }
        public Guid UserDtoId { get; set; }
        
        public DateTimeOffset CommentedIn { get; set; }
        public string Text { get; set; }
    }
}
