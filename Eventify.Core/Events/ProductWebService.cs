using Eventify.Common.Classes.Events;
using Eventify.Common.Classes.Logger;
using Eventify.Common.Classes.Messages.RequestResult;
using Eventify.Core.Base.Services;
using Eventify.DAL.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventify.Core.Events
{
    public interface IEventWebService
    {
        Task<RequestResult<IEnumerable<Event>>> GetEvents();
    }

    internal class EventWebService : BaseWebService, IEventWebService
    {
        private readonly IEventRepository _eventRepository;

        public EventWebService(
            ILogger logger,
            IEventRepository eventRepository) : base(logger)
        {
            _eventRepository = eventRepository;
        }

        public async Task<RequestResult<IEnumerable<Event>>> GetEvents()
        {
            try
            {
                var events = await _eventRepository.GetEvents();
                return RequestResult<IEnumerable<Event>>.CreateSuccess(events);
            }
            catch (Exception e)
            {
                return HandleException<IEnumerable<Event>>(e);
            }
        }
    }
}
