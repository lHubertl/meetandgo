using MeetAndGo.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetAndGoApi.BusinessLayer.DbContexts
{
    public class MeetAndGoContext : DbContext
    {
        #region Db sets

        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<EventModel> Events { get; set; }
        public DbSet<MemberModel> Members { get; set; }
        public DbSet<PointModel> Points { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<VoteModel> Votes { get; set; }

        #endregion

        #region Constructor

        public MeetAndGoContext(DbContextOptions<MeetAndGoContext> options) : base(options)
        {
            
        }

        #endregion
    }
}
