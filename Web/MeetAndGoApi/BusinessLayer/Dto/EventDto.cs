using System;
using MeetAndGo.Shared.Models;

namespace MeetAndGoApi.BusinessLayer.Dto
{
    public class EventDto : EventModel, IDtoBase
    {
        public Guid Id { get; set; }
    }
}
