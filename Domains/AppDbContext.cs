using Microsoft.EntityFrameworkCore;

namespace SomeFacts.Domains
{
    public class AppDbContext : DbContext
    {
        public DbSet<Fact> Facts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}
