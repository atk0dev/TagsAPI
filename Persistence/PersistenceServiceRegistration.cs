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
            services.Configure<DatabaseContext>(
                configuration.GetSection(nameof(DatabaseContext)));

            services.AddSingleton<IDatabaseContext>(sp =>
                sp.GetRequiredService<IOptions<DatabaseContext>>().Value);

            services.AddSingleton<IMongoClient, MongoClient>(sp =>
                new MongoClient(configuration.GetSection("DatabaseContext:ConnectionString").Value));

            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddTransient<TagRepository>();
            
            services.AddTransient(typeof(ITagService), typeof(TagService));
            
            return services;    
        }
    }
}
