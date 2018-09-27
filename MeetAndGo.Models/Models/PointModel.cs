using System;

namespace MeetAndGo.Shared.Models
{
    public class PointModel
    {
        public Guid PointModelId { get; set; }
        public string Name { get; set; }
        public double Long { get; set; }
        public double Lat { get; set; }
    }
}
