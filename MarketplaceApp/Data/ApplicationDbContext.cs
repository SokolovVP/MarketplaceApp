using MarketplaceApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace MarketplaceApp.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<User> user { get; set; }
        public DbSet<Quest> quest { get; set; }
        public DbSet<Solution> solution { get; set; }
        public DbSet<Comment> comment { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quest>().HasOne(q => q.user);
        }
    }
}
