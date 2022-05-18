using Eventify.Common.Utils.Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Common.Utils
{
    public class IoC
    {
        public static void Init(IServiceCollection services)
        {
            services.AddSingleton<ILogger, EverywhereLogger>();
        }
    }
}
