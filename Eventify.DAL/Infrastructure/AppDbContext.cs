using Eventify.Domain;
using Microsoft.EntityFrameworkCore;

namespace Eventify.DAL.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<DbEvent> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
