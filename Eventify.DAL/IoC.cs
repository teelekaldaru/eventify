using Eventify.DAL.Products;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.DAL
{
    public class IoC
    {
        public static void Init(IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
