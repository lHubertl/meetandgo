namespace MeetAndGo.Shared.Models
{
    public class MemberModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public double OrganizerRating { get; set; }
        public double MemberRating { get; set; }
        public string CompressedPhotoUrl { get; set; }
        public string HighQualityPhoto { get; set; }
    }
}
