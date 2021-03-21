using System.Linq;
using EmailApp.DataAccess.EfModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmailApp.IntegrationTests
{
    public class SqliteWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        private readonly SqliteConnection _connection;

        public SqliteWebApplicationFactory()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // get the entry the regular Startup method added from AddDbContext and replace it
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<EmailContext>));
                services.Remove(descriptor);

                services.AddDbContext<EmailContext>(options =>
                {
                    options.UseSqlite(_connection);
                });

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<EmailContext>();

                db.Database.EnsureCreated();

                // if we needed initial data different from what the context has in HasData calls, then...
                //var logger = scopedServices
                //    .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();
                //try
                //{
                //    Utilities.InitializeDbForTests(db);
                //}
                //catch (Exception ex)
                //{
                //    logger.LogError(ex, "An error occurred seeding the " +
                //        "database with test messages. Error: {Message}", ex.Message);
                //}
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _connection.Close();
        }
    }
}
