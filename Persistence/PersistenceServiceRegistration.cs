namespace Persistence
{
    using Application.Contracts.Persistence;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using Persistence.Repositories;
    using Persistence.Services;
    using Persistence.Settings;

    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TagsDatabaseSettings>(
                configuration.GetSection(nameof(TagsDatabaseSettings)));

            services.AddSingleton<ITagsDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<TagsDatabaseSettings>>().Value);

            services.AddSingleton<IMongoClient, MongoClient>(sp =>
                new MongoClient(configuration.GetSection("TagsDatabaseSettings:ConnectionString").Value));

            services.AddSingleton(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddSingleton(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddSingleton<TagRepository>();
            services.AddSingleton(typeof(ITagService), typeof(TagService));
            
            return services;    
        }
    }
}
