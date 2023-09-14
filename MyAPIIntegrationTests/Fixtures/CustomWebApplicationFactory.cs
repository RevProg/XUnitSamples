using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MyAPI.Services;
using MyAPIIntegrationTests.Overrides;

namespace MyAPIIntegrationTests.Fixtures;

// https://xunit.net/docs/shared-context
public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            //var descriptor = services.SingleOrDefault(o => o.ServiceType == typeof(IMyService));
            //if (descriptor != null)
            //    services.Remove(descriptor); 

            services.AddSingleton<IMyService, MyTestService>();
        });
    }
}
