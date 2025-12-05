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
        public DbSet<Travel> TravelDestinations{get;set;}
    }
}
