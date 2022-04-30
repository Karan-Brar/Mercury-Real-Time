using Microsoft.EntityFrameworkCore;

namespace MercuryMVC.Models
{
    public class ChattingContext : DbContext
    {
        public ChattingContext(DbContextOptions<ChattingContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();

            modelBuilder.Entity<AppUser>().HasIndex(appUser => appUser.UserName).IsUnique();
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
