using System;
using System.Collections.Generic;
using MeetAndGo.Shared.Enums;

namespace MeetAndGoApi.Infrastructure.Dto
{
    public class EventDto
    {
        public Guid EventDtoId { get; set; }

        public List<EventUser> EventUsers { get; set; }
        public List<PointDto> PointDtos { get; set; }
        public List<CommentDto> CommentDtos { get; set; }
        
        public string Name { get; set; }
        public EventStates EventState { get; set; }
        public Transports Transport { get; set; }
        public int MaxSeats { get; set; }
        public double TotalPrice { get; set; }
        public string CurrencyCode { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public double ExpectedRating { get; set; }
        public string Description { get; set; }
    }
}
