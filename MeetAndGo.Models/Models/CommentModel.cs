using System;

namespace MeetAndGo.Shared.Models
{
    public class CommentModel
    {
        public Guid CommentModelId { get; set; }
        public MemberModel Author { get; set; }
        public EventModel Event { get; set; }
        public DateTimeOffset CommentedIn { get; set; }
        public string Text { get; set; }
    }
}
