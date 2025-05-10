using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Testcontainers.MsSql;
using User.Infrastructure.Data;

namespace User.IntegrationTests
{
    public class UserIntegrationTestWebFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MsSqlContainer _container = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server")
            .WithPassword("SwN12345678*")
            .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var descriptor = services
                .SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<UserDbContext>))
                //.AddMediatR(typeof(AssemblyMarker).Assembly);
                ;

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<UserDbContext>(options =>
                {
                    options.UseSqlServer(_container.GetConnectionString());

                });
            });
        
        }

        public new Task DisposeAsync()
        {
            return _container.StopAsync(); 
            
        }

        public Task InitializeAsync()
        {
            return _container.StartAsync();
        }

    }
}
