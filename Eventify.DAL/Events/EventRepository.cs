using System;
using Eventify.Common.Classes.Events;
using Eventify.DAL.Base;
using Eventify.DAL.Infrastructure;
using Eventify.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventify.Common.Classes.Exceptions;

namespace Eventify.DAL.Events
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetEvents();

        Task<Event> GetEventById(Guid eventId);

        Task<Event> AddEvent(Event entity);

        Task<Event> UpdateEvent(Event entity);

        Task DeleteEvent(Guid eventId);
    }

    internal class EventRepository : BaseRepository<AppDbContext>, IEventRepository
    {
        public EventRepository(DbContextOptions options) : base(options)
        {
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            var dbEvents = await GetAllAsync<DbEvent>();
            return dbEvents.Select(x => x.ToEvent());
        }

        public async Task<Event> GetEventById(Guid eventId)
        {
            await using var context = CreateContext();
            var dbEvent = await context.Events
                .Include(x => x.Attendees).ThenInclude(x => x.Person)
                .Include(x => x.Attendees).ThenInclude(x => x.Company)
                .FirstOrDefaultAsync(x => x.Id == eventId);

            return dbEvent.ToEvent();
        }

        public async Task<Event> AddEvent(Event entity)
        {
            var dbEvent = entity.ToDbEvent();
            var added = await AddAsync(dbEvent);
            return added.ToEvent();
        }

        public async Task<Event> UpdateEvent(Event entity)
        {
            var dbEvent = entity.ToDbEvent();
            var updated = await UpdateAsync(dbEvent);
            return updated.ToEvent();
        }

        public async Task DeleteEvent(Guid eventId)
        {
            var dbEvent = await GetAsync<DbEvent>(eventId);
            if (dbEvent == null)
            {
                throw new SimpleException($"Event with id {eventId} does not exist");
            }

            await RemoveAsync(dbEvent);
        }
    }
}
