namespace IntegrationTests.Base
{
    using System;
    using System.Net.Http;
    using Api.Services;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using MongoDB.Driver;
    using Persistence.Repositories;
    using Persistence.Services;
    using Persistence.Settings;

    public class CustomWebApplicationFactory<TStartup>
            : WebApplicationFactory<TStartup> where TStartup : class
    {
        public HttpClient GetAnonymousClient()
        {
            return this.CreateClient();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var sp = services.BuildServiceProvider();
                
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    var baseService = new BaseService<Domain.Entities.Tag>(
                        new MongoClient("mongodb://localhost:27017"),
                        new BaseRepository<Domain.Entities.Tag>(), 
                        new LoggedInUserService(), 
                        new DatabaseContext
                        {
                            ConnectionString = "mongodb://localhost:27017",
                            DatabaseName = $"TagsDb",
                        });

                    services.AddSingleton(baseService);

                    try
                    {
                        Utilities.InitializeDbForTests(baseService);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"An error occurred seeding the database with test messages. Error: {ex.Message}");
                    }
                };
            });
        }
    }
}
