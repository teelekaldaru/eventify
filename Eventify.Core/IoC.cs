﻿using Eventify.Core.Attendees;
using Eventify.Core.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Core
{
    public class IoC
    {
        public static void Init(IServiceCollection services)
        {
            services.AddScoped<IEventWebService, EventWebService>();
            services.AddScoped<IAttendeeWebService, AttendeeWebService>();
        }
    }
}
