using System;

namespace MeetAndGoApi.Infrastructure.Dto
{
    public class FileDto
    {
        public Guid FileDtoId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
