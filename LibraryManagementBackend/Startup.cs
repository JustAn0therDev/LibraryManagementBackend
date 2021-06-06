using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repositories;
using Repositories.Interfaces;
using UseCases;
using UseCases.Interfaces;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using LibraryManagementBackend.Controllers;
using System;

namespace LibraryManagementBackend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString;
            services.AddControllers();

            services.AddTransient<IBookUseCase, BookUseCase>();
            services.AddTransient<IAuthorUseCase, AuthorUseCase>();
            services.AddTransient<IPublisherUseCase, PublisherUseCase>();
            services.AddTransient<IGenreUseCase, GenreUseCase>();
            services.AddSingleton<ILogger, Logger<BookController>>();

            connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

            if (string.IsNullOrEmpty(connectionString) || string.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = GetConnectionStringForCurrentOperatingSystem();
            }

            services.AddDbContext<LibraryContext>(options => options.UseSqlite(connectionString));

            services.AddScoped<IBookRepository, LibraryContext>();
            services.AddScoped<IAuthorRepository, LibraryContext>();
            services.AddScoped<IGenreRepository, LibraryContext>();
            services.AddScoped<IPublisherRepository, LibraryContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public string GetConnectionStringForCurrentOperatingSystem() 
        {
            bool isRunningOnWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            if (isRunningOnWindows) 
            {
                return Configuration.GetConnectionString("sqlite");
            }

            return Configuration.GetConnectionString("sqliteUnix");
        }
    }
}
