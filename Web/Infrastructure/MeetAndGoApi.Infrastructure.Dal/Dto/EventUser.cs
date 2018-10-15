using System;

namespace MeetAndGoApi.Infrastructure.Dal.Dto
{
    public class EventUser
    {
        public Guid EventUserId { get; set; }

        public EventDto EventDto { get; set; }
        public Guid EventDtoId { get; set; }

        public UserDto UserDto { get; set; }
        public Guid UserDtoId { get; set; }
    }
}
