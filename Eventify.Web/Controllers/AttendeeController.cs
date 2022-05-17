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

        [HttpGet("{attendeeId}")]
        public async Task<JsonResult> GetAttendee(Guid attendeeId)
        {
            var result = await _attendeeWebService.GetAttendeeDetails(attendeeId);
            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<JsonResult> SaveEvent([FromBody] AttendeeSaveModel saveModel)
        {
            var result = await _attendeeWebService.SaveAttendee(saveModel);
            return new JsonResult(result);
        }

        [HttpDelete("{attendeeId}")]
        public async Task<JsonResult> DeleteEvent(Guid attendeeId)
        {
            var result = await _attendeeWebService.DeleteAttendee(attendeeId);
            return new JsonResult(result);
        }
    }
}
