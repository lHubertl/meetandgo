using System;
using System.Collections.Generic;
using MeetAndGo.Shared.Enums;

namespace MeetAndGo.Shared.Models
{
    public class EventModel
    {
        public string Name { get; set; }
        public EventStates EventState { get; set; }
        public IEnumerable<PointModel> Direction { get; set; }
        public Transports Transport { get; set; }
        public IEnumerable<MemberModel> Members { get; set; }
        public int MaxSeats { get; set; }
        public double TotalPrice { get; set; }
        public string CurrencyCode { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public double ExpectedRating { get; set; }
        public string Description { get; set; }
        public IEnumerable<CommentModel> Comments { get; set; }
    }
}
