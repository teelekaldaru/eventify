using System;
using System.Threading.Tasks;
using Eventify.Common.Classes.Attendees;
using Eventify.Common.Classes.Exceptions;
using Eventify.DAL.Base;
using Eventify.DAL.Infrastructure;
using Eventify.Domain;
using Microsoft.EntityFrameworkCore;

namespace Eventify.DAL.Attendees
{
    public interface IAttendeeRepository
    {
        Task<Attendee> GetAttendeeById(Guid attendeeId);

        Task<Attendee> AddAttendee(Attendee attendee);

        Task DeleteAttendee(Guid attendeeId);
    }

    internal class AttendeeRepository : BaseRepository<AppDbContext>, IAttendeeRepository
    {
        public AttendeeRepository(DbContextOptions options) : base(options)
        {
        }

        public async Task<Attendee> GetAttendeeById(Guid attendeeId)
        {
            await using var context = CreateContext();
            var dbAttendee = await context.Attendees
                .Include(x => x.Person)
                .Include(x => x.Company)
                .FirstOrDefaultAsync(x => x.Id == attendeeId);
            
            return dbAttendee.ToAttendee();
        }

        public async Task<Attendee> AddAttendee(Attendee attendee)
        {
            await using var context = CreateContext();
            await using var transaction = context.Database.BeginTransaction();

            var dbAttendee = attendee.ToDbAttendee();
            if (attendee.Person != null)
            {
                var dbPerson = attendee.Person.ToDbPerson();
                dbAttendee.PersonId = context.Persons.Add(dbPerson).Entity.Id;
            }

            if (attendee.Company != null)
            {
                var dbCompany = attendee.Company.ToDbCompany();
                dbAttendee.CompanyId = context.Companies.Add(dbCompany).Entity.Id;
            }

            dbAttendee = context.Attendees.Add(dbAttendee).Entity;

            await context.SaveChangesAsync();
            transaction.Commit();

            return dbAttendee.ToAttendee();
        }

        public async Task DeleteAttendee(Guid attendeeId)
        {
            var dbAttendee = await GetAsync<DbAttendee>(attendeeId);
            if (dbAttendee == null)
            {
                throw new SimpleException($"Attendee with id {attendeeId} does not exist");
            }

            await RemoveAsync(dbAttendee);
        }
    }
}
