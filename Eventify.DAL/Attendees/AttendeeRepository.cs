using System;
using System.Threading.Tasks;
using Eventify.Common.Classes.Attendees;
using Eventify.Common.Utils.Exceptions;
using Eventify.DAL.Base;
using Eventify.DAL.Infrastructure;
using Eventify.Domain;
using Microsoft.EntityFrameworkCore;

namespace Eventify.DAL.Attendees
{
	public interface IAttendeeRepository
    {
        Task<EventAttendee?> GetEventAttendeeById(Guid eventAttendeeId);

        Task<EventAttendee> AddEventAttendee(EventAttendee eventAttendee);

        Task<EventAttendee> UpdateEventAttendee(Guid id, EventAttendeeUpdateSet updateSet);

        Task DeleteEventAttendee(Guid eventAttendeeId);
    }

    internal class AttendeeRepository : BaseRepository<AppDbContext>, IAttendeeRepository
    {
        public AttendeeRepository(DbContextOptions options) : base(options)
        {
        }

        public async Task<EventAttendee?> GetEventAttendeeById(Guid eventAttendeeId)
        {
            await using var context = CreateContext();
            var dbEventAttendee = await context.EventAttendees
	            .Include(x => x.Attendee)
	            .FirstOrDefaultAsync(x => x.Id == eventAttendeeId);
            
            return dbEventAttendee.ToEventAttendee();
        }

        public async Task<EventAttendee> AddEventAttendee(EventAttendee eventAttendee)
        {
            await using var context = CreateContext();
            await using var transaction = await context.Database.BeginTransactionAsync();

            var dbAttendee = eventAttendee.Attendee.ToDbAttendee();
            var dbAttendeeEntity = context.Attendees.Add(dbAttendee).Entity;

            var dbEventAttendee = eventAttendee.ToDbEventAttendee();
            dbEventAttendee.AttendeeId = dbAttendeeEntity.Id;
            var dbEventAttendeeEntity = context.EventAttendees.Add(dbEventAttendee).Entity;

            await context.SaveChangesAsync();
            await transaction.CommitAsync();

            return dbEventAttendeeEntity.ToEventAttendee();
        }

        public async Task<EventAttendee> UpdateEventAttendee(Guid id, EventAttendeeUpdateSet updateSet)
        {
	        await using var context = CreateContext();

	        var dbEventAttendee = await context.EventAttendees
		        .Include(x => x.Attendee)
		        .FirstOrDefaultAsync(x => x.Id == id);

	        if (dbEventAttendee == null)
	        {
		        throw new SimpleException($"Event attendee with id {id} does not exist");
	        }

            dbEventAttendee.ApplyUpdateSet(updateSet);
            context.Entry(dbEventAttendee).State = EntityState.Modified;

            await context.SaveChangesAsync();
            return dbEventAttendee.ToEventAttendee();
        }

        public async Task DeleteEventAttendee(Guid eventAttendeeId)
        {
            var dbEventAttendee = await GetAsync<DbEventAttendee>(eventAttendeeId);
            if (dbEventAttendee == null)
            {
                throw new SimpleException($"Event attendee with id {eventAttendeeId} does not exist");
            }

            await RemoveAsync(dbEventAttendee);
        }
    }
}
