using Eventify.DAL.Attendees;
using Eventify.DAL.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.DAL
{
    public class IoC
    {
        public static void Init(IServiceCollection services)
        {
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IAttendeeRepository, AttendeeRepository>();
        }
    }
}
