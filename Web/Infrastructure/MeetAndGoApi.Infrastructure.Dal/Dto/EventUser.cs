using System;

namespace MeetAndGoApi.Infrastructure.Dal.Dto
{
    public class EventUser
    {
        public Guid EventUserId { get; set; }

        public EventDto EventDto { get; set; }
        public Guid EventDtoId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
