using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StockTicker.Service.Models;

namespace StockTickerUnitTests.Factory
{
    public class ApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.UseSetting("Auth:DisablePermissions", "true");
            builder.UseStartup<TestStartup>();
            builder.ConfigureServices(services =>
            {
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                  .AddEntityFrameworkInMemoryDatabase()
                  .BuildServiceProvider();

                // Add a database context using an in-memory database for testing.
                services.AddDbContext<ReportDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDb");
                options.UseInternalServiceProvider(serviceProvider);
            });
            });
        }
    }
}
