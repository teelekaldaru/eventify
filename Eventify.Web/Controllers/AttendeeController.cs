using System;
using System.Threading.Tasks;
using Eventify.Core.Attendees;
using Microsoft.AspNetCore.Mvc;

namespace Eventify.Web.Controllers
{
    [Route("api/attendees")]
    public class AttendeeController : ControllerBase
    {
        private readonly IAttendeeWebService _attendeeWebService;

        public AttendeeController(IAttendeeWebService attendeeWebService)
        {
            _attendeeWebService = attendeeWebService;
        }

        [HttpGet("{eventAttendeeId}")]
        public async Task<JsonResult> GetEventAttendee(Guid eventAttendeeId)
        {
            var result = await _attendeeWebService.GetEventAttendee(eventAttendeeId);
            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<JsonResult> SaveEvent([FromBody] AttendeeSaveModel saveModel)
        {
            var result = await _attendeeWebService.SaveAttendee(saveModel);
            return new JsonResult(result);
        }

        [HttpDelete("{eventAttendeeId}")]
        public async Task<JsonResult> RemoveAttendeeFromEvent(Guid eventAttendeeId)
        {
            var result = await _attendeeWebService.RemoveAttendeeFromEvent(eventAttendeeId);
            return new JsonResult(result);
        }
    }
}
