using System;
using System.Threading.Tasks;
using Eventify.Core.Events;
using Microsoft.AspNetCore.Mvc;

namespace Eventify.Web.Controllers
{
    [Route("api/events")]
    public class EventController : ControllerBase
    {

        private readonly IEventWebService _eventWebService;

        public EventController(IEventWebService eventWebService)
        {
            _eventWebService = eventWebService;
        }

        [HttpGet]
        public async Task<JsonResult> GetEvents()
        {
            var result = await _eventWebService.GetEvents();
            return new JsonResult(result);
        }

        [HttpGet("{eventId}")]
        public async Task<JsonResult> GetEvent(Guid eventId)
        {
            var result = await _eventWebService.GetEventDetails(eventId);
            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<JsonResult> SaveEvent([FromBody] EventSaveModel saveModel)
        {
            var result = await _eventWebService.SaveEvent(saveModel);
            return new JsonResult(result);
        }

        [HttpDelete("{eventId}")]
        public async Task<JsonResult> DeleteEvent(Guid eventId)
        {
            var result = await _eventWebService.DeleteEvent(eventId);
            return new JsonResult(result);
        }
    }
}
