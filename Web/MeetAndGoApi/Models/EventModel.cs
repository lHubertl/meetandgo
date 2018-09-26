using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeetAndGoApi.Enums;

namespace MeetAndGoApi.Models
{
    public class EventModel
    {
        public string Name { get; set; }
        public EventStates EventState { get; set; }
    }
}
