using System.Reflection;

using HelloWorld.Api.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Serilog;

namespace HelloWorld.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(_configuration)
               .CreateLogger();

            services.AddControllers();

            services.AddNameHolder(_configuration);
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }

        public void Configure(IApplicationBuilder application)
        {
            application
               .UseRouting()
               .UseEndpoints(endpoints => endpoints.MapControllers())
               .UseSerilogRequestLogging();
        }
    }
}