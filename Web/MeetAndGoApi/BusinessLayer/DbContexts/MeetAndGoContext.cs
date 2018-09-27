using MeetAndGoApi.BusinessLayer.Dto;
using Microsoft.EntityFrameworkCore;

namespace MeetAndGoApi.BusinessLayer.DbContexts
{
    public class MeetAndGoContext : DbContext
    {
        #region Constructor

        public MeetAndGoContext(DbContextOptions<MeetAndGoContext> options) : base(options)
        {
            
        }

        #endregion

        #region Db sets

        public DbSet<CommentDto> Comments { get; set; }
        public DbSet<EventDto> Events { get; set; }
        public DbSet<MemberDto> Members { get; set; }
        public DbSet<PointDto> Points { get; set; }
        public DbSet<UserDto> Users { get; set; }
        public DbSet<VoteDto> Votes { get; set; }

        #endregion
    }
}
