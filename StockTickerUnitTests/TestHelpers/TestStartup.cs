using StockTicker.Service.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockTicker;

namespace StockTickerUnitTests.Tests.Helpers
{
    //To be used if host diffent to normal Startup
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        { }

        public void RegisterServices(IServiceCollection services)
        {
            RegisterTestDbContext(services);
        }

        private static void RegisterTestDbContext(IServiceCollection services)
        {
            // Build the service provider.
            var sp = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database contexts
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var appDb = scopedServices.GetRequiredService<CompanyContext>();
            
            // Ensure the database is created.
            appDb.Database.EnsureCreated();

            //Set up test data
            TestUtils.InitializeDbForTests(appDb);
        }        
    }
}