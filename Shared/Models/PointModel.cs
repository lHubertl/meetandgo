namespace MeetAndGo.Shared.Models
{
    public class PointModel
    {
        public string Name { get; set; }
        public double Long { get; set; }
        public double Lat { get; set; }
        public PointType Type { get; set; }
    }

    public enum PointType
    {
        Searched = 0,
        Home,
        Work,
        Saved,
        Recently
    }
}
