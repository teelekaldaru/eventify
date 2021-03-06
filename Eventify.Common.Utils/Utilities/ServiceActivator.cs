using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Common.Utils.Utilities
{
    public static class ServiceActivator
    {
        private static IServiceProvider _serviceProvider;

        public static void AddServiceActivator(this IApplicationBuilder app)
        {
            if (_serviceProvider == null)
            {
                _serviceProvider = app.ApplicationServices;
            }
        }

        public static IServiceScope GetScope(IServiceProvider serviceProvider = null)
        {
            return (serviceProvider ?? _serviceProvider)?.GetRequiredService<IServiceScopeFactory>().CreateScope();
        }
    }
}
