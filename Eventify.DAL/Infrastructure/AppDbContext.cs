using Eventify.Common.Utils.Database;
using Eventify.Domain;
using Microsoft.EntityFrameworkCore;

namespace Eventify.DAL.Infrastructure
{
    public class AppDbContext : BaseDbContext
    {
        public DbSet<DbEvent> Events { get; set; }

        public DbSet<DbEventAttendee> EventAttendees { get; set; }

        public DbSet<DbAttendee> Attendees { get; set; }

        public DbSet<DbPaymentMethod> PaymentMethods { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DbEventAttendee>().HasOne(x => x.Event).WithMany(x => x.EventAttendees).HasForeignKey(x => x.EventId);
            builder.Entity<DbEventAttendee>().HasOne(x => x.Attendee).WithMany().HasForeignKey(x => x.AttendeeId);

            builder.Entity<DbEventAttendee>().HasOne<DbPaymentMethod>().WithMany().HasForeignKey(x => x.PaymentMethod);
        }
    }
}
