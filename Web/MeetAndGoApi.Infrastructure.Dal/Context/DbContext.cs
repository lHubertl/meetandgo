using MeetAndGoApi.Infrastructure.Dal.DataSeed;
using MeetAndGoApi.Infrastructure.Dto;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MeetAndGoApi.Infrastructure.Dal.Context
{
    public class DbContext : IdentityDbContext<ApplicationUser>
    {
        #region Db sets

        public DbSet<CommentDto> Comments { get; set; }
        public DbSet<EventDto> Events { get; set; }
        public DbSet<PointDto> Points { get; set; }
        public DbSet<VoteDto> Votes { get; set; }
        public DbSet<FileDto> Files { get; set; }

        #endregion

        #region Constructor

        public DbContext(DbContextOptions<DbContext> options) : base(options)
        {
            var dbSeed = new DbSeed();
            dbSeed.InitializeIfNeeded(this);
        }

        #endregion

        #region Override methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User - Vote One-to-Many

            modelBuilder.Entity<VoteDto>()
                .HasOne(pt => pt.ApplicationUser).
                WithMany(p => p.VoteDtos).
                HasForeignKey(pt => pt.ApplicationUserId);

            // User - Comment One-to-Many

            modelBuilder.Entity<CommentDto>()
                .HasOne(pt => pt.ApplicationUser).
                WithMany(p => p.CommentDtos).
                HasForeignKey(pt => pt.ApplicationUserId);

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
                .HasOne(pt => pt.ApplicationUser)
                .WithMany(t => t.EventUsers)
                .HasForeignKey(pt => pt.ApplicationUserId);

            // ApplicationUser - File One-to-One

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(pt => pt.FileDto)
                .WithOne(p => p.ApplicationUser)
                .HasForeignKey<FileDto>(pt => pt.ApplicationUserId);
        }

        #endregion
    }
}
