using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SomeFacts.Models;

namespace SomeFacts.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<Fact> Facts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

    }
}
