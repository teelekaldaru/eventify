using Eventify.Core.Products;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Core
{
    public class IoC
    {
        public static void Init(IServiceCollection services)
        {
            services.AddScoped<IProductWebService, ProductWebService>();
        }
    }
}
