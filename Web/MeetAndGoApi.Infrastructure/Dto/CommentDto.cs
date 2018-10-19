using System;

namespace MeetAndGoApi.Infrastructure.Dto
{
    public class CommentDto
    {
        public Guid CommentDtoId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public Guid EventDtoId { get; set; }

        public EventDto EventDto { get; set; }
        public string ApplicationUserId { get; set; }
        
        public DateTimeOffset CommentedIn { get; set; }
        public string Text { get; set; }
    }
}
