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
    }
}
