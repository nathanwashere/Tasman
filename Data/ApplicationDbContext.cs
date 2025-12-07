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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Email as primary key for User
            modelBuilder.Entity<User>()
                .HasKey(u => u.Email);
        }
    }
}
