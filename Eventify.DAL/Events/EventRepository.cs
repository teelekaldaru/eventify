using Eventify.Common.Classes.Events;
using Eventify.DAL.Base;
using Eventify.DAL.Infrastructure;
using Eventify.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventify.DAL.Events
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetEvents();
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
    }
}
