using Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using Api.Services;
using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Persistence.Repositories;
using Persistence.Services;
using Persistence.Settings;

namespace IntegrationTests.Base
{
    public class CustomWebApplicationFactory<TStartup>
            : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var sp = services.BuildServiceProvider();
                
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    var baseService = new BaseService<Tag>(
                        new BaseRepository<Tag>(), 
                        new LoggedInUserService(), 
                        new TagsDatabaseSettings
                        {
                            ConnectionString = "mongodb://admin:password@localhost:27017",
                            DatabaseName = $"TagsDb",
                            TagsCollectionName = $"Tags"
                        });

                    services.AddSingleton(baseService);

                    try
                    {
                        //Utilities.InitializeDbForTests(baseService);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"An error occurred seeding the database with test messages. Error: {ex.Message}");
                    }
                };
            });
        }

        public HttpClient GetAnonymousClient()
        {
            return CreateClient();
        }

    }
}
