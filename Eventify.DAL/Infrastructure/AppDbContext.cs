using Eventify.Domain;
using Microsoft.EntityFrameworkCore;

namespace Eventify.DAL.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<DbEvent> Events { get; set; }

        public DbSet<DbAttendee> Attendees { get; set; }

        public DbSet<DbCompany> Companies { get; set; }

        public DbSet<DbPerson> Persons { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DbAttendee>().HasOne(x => x.Event).WithMany(x => x.Attendees).HasForeignKey(x => x.EventId);
            builder.Entity<DbAttendee>().HasOne(x => x.Person).WithMany().HasForeignKey(x => x.PersonId);
            builder.Entity<DbAttendee>().HasOne(x => x.Company).WithMany().HasForeignKey(x => x.CompanyId);
            builder.Entity<DbAttendee>().HasOne<DbPaymentMethod>().WithMany().HasForeignKey(x => x.PaymentMethod);
        }
    }
}
