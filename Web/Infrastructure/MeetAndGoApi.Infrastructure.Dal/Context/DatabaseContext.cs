using MeetAndGoApi.Infrastructure.Dal.DataSeed;
using MeetAndGoApi.Infrastructure.Dal.Dto;
using Microsoft.EntityFrameworkCore;

namespace MeetAndGoApi.Infrastructure.Dal.Context
{
    public class DatabaseContext : DbContext
    {
        #region Db sets

        public DbSet<CommentDto> Comments { get; set; }
        public DbSet<EventDto> Events { get; set; }
        public DbSet<PointDto> Points { get; set; }
        public DbSet<UserDto> Users { get; set; }
        public DbSet<VoteDto> Votes { get; set; }

        #endregion

        #region Constructor

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            var dbSeed = new DatabaseSeed();
            dbSeed.InitializeIfNeeded(this);
        }

        #endregion

        #region Override methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User - Vote One-to-Many

            modelBuilder.Entity<VoteDto>()
                .HasOne(pt => pt.UserDto).
                WithMany(p => p.VoteDtos).
                HasForeignKey(pt => pt.UserDtoId);

            // User - Comment One-to-Many

            modelBuilder.Entity<CommentDto>()
                .HasOne(pt => pt.UserDto).
                WithMany(p => p.CommentDtos).
                HasForeignKey(pt => pt.UserDtoId);

            // Comment - Event One-to-Many

            modelBuilder.Entity<CommentDto>()
                .HasOne(pt => pt.EventDto).
                WithMany(p => p.CommentDtos).
                HasForeignKey(pt => pt.EventDtoId);

            // Point - Event One-to-Many

            modelBuilder.Entity<PointDto>()
                .HasOne(pt => pt.EventDto)
                .WithMany(p => p.PointDtos)
                .HasForeignKey(pt => pt.EventDtoId);
            
            // Event User Many-to-Many

            modelBuilder.Entity<EventUser>()
                .HasOne(pt => pt.EventDto)
                .WithMany(p => p.EventUsers)
                .HasForeignKey(pt => pt.EventDtoId);

            modelBuilder.Entity<EventUser>()
                .HasOne(pt => pt.UserDto)
                .WithMany(t => t.EventUsers)
                .HasForeignKey(pt => pt.UserDtoId);
        }

        #endregion
    }
}
