using System;

namespace MeetAndGoApi.Infrastructure.Dal.Dto
{
    public class PointDto
    {
        public Guid PointDtoId { get; set; }

        public EventDto EventDto { get; set; }
        public Guid EventDtoId { get; set; }
        
        public string Name { get; set; }
        public double Long { get; set; }
        public double Lat { get; set; }
    }
}
