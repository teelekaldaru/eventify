using AutoMapper;
using Eventify.Common.Classes.AutoMapper;
using Eventify.Common.Utils;
using Eventify.Common.Utils.Utilities;
using Eventify.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.UnitTests
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration config)
        {
            Configuration = new ConfigurationBuilder().Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            var configuration = new MapperConfiguration(cfg => cfg.AddMaps(AssemblyHelper.GetAssemblies()));
            MapperWrapper.Initialize(configuration);
            IoC.Init(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.AddServiceActivator();
        }
    }
}
