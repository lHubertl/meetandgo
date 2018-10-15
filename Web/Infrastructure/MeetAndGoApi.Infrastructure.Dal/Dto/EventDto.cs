using System;
using System.Collections.Generic;
using AutoMapper.Configuration.Conventions;
using MeetAndGo.Shared.Enums;
using MeetAndGo.Shared.Models;

namespace MeetAndGoApi.Infrastructure.Dal.Dto
{
    public class EventDto
    {
        [MapTo(nameof(EventModel.Id))]
        public Guid EventDtoId { get; set; }

        [MapTo(nameof(EventModel.Members))]
        public ICollection<EventUser> EventUsers { get; set; }

        [MapTo(nameof(EventModel.Direction))]
        public ICollection<PointDto> PointDtos { get; set; }

        [MapTo(nameof(EventModel.Comments))]
        public ICollection<CommentDto> CommentDtos { get; set; }
        
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
