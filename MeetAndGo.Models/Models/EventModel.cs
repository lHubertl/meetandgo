using System;
using System.Collections.Generic;
using MeetAndGo.Shared.Enums;

namespace MeetAndGo.Shared.Models
{
    public class EventModel
    {
        public string Id { get; set; }
        public List<MemberModel> Members { get; set; }
        public List<PointModel> Direction { get; set; }
        public List<CommentModel> Comments { get; set; }

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
