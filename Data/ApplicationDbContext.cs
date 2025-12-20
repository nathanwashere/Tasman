using Microsoft.EntityFrameworkCore;
using Tasman.Models;

namespace Tasman.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Travel> TravelDestinations { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<WaitlistEntry> WaitlistEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Email as primary key for User
            modelBuilder.Entity<User>()
                .HasKey(u => u.Email);

            modelBuilder.Entity<Travel>()
                .Property(t => t.IsVisible)
                .HasDefaultValue(true);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Travel)
                .WithMany()
                .HasForeignKey(b => b.TravelId);

            modelBuilder.Entity<WaitlistEntry>()
                .HasOne(w => w.Travel)
                .WithMany()
                .HasForeignKey(w => w.TravelId);
        }
    }
}
