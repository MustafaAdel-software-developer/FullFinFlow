using FinFlow.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FinFlow.IntegrationTests
{
    public abstract class IntegrationTestBase : IClassFixture<WebApplicationFactory<Program>>
    {
        protected readonly WebApplicationFactory<Program> Factory;
        protected readonly HttpClient Client;
        //protected IntegrationTestBase(WebApplicationFactory<Program> factory)
        //{
        //    Factory = factory.WithWebHostBuilder(builder =>
        //    {
        //        builder.ConfigureServices(services =>
        //        {
        //            // Replace real database with in-memory for testing
        //            var descriptor = services.SingleOrDefault(
        //                d => d.ServiceType == typeof(DbContextOptions<FinanceDbContext>));

        //            if (descriptor != null)
        //                services.Remove(descriptor);

        //            services.AddDbContext<FinanceDbContext>(options =>
        //            {
        //                options.UseInMemoryDatabase("TestDb");
        //            });
        //        });
        //    });

        //    Client = Factory.CreateClient();
        //}
    }
}