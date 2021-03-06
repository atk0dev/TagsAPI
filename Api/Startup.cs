namespace Api
{
    using Api.Middleware;
    using Api.Services;
    using Application;
    using Application.Contracts;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.Server.Features;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.OpenApi.Models;
    using Persistence;

    public class Startup
    { 
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            this.AddSwagger(services);

            services.AddApplicationServices();
            services.AddPersistenceServices(this.Configuration);
            
            services.AddSingleton<ILoggedInUserService, LoggedInUserService>();

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime, ILogger<Startup> logger)
        {
            lifetime.ApplicationStarted.Register(
                () => LogAddresses(app.ServerFeatures, logger));
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo API");
            });
            
            app.UseCustomExceptionHandler();

            app.UseCors("Open");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void LogAddresses(IFeatureCollection features, ILogger logger)
        {
            var addressFeature = features.Get<IServerAddressesFeature>();

            if (addressFeature != null)
            {
                foreach (var addresses in addressFeature.Addresses)
                {
                    logger.LogInformation("Listening on address: " + addresses);
                }
            }
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }
    }
}
