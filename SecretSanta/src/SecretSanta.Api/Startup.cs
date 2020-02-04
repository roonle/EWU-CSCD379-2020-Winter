using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SecretSanta.Business;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using Microsoft.Data.Sqlite;
using AutoMapper;


namespace SecretSanta.Api
{
    // Justification: Disable until ConfigureServices is added back.
#pragma warning disable CA1052 // Static holder types should be Static or NotInheritable
    public class Startup
    #pragma warning restore CA1052 // Static holder types should be Static or NotInheritable
    {
        public static void ConfigureServices(IServiceCollection services)
        {
#pragma warning disable CA2000 // connection is used further out
            var sqliteConnection = new SqliteConnection("DataSource=:memory:");
#pragma warning restore CA2000 // connection is used further out

            sqliteConnection.Open();

            services.AddDbContext<ApplicationDbContext>(options => options.EnableSensitiveDataLogging()
            .UseSqlite(sqliteConnection));

            services.AddScoped<IGiftService, GiftService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGroupService, GroupService>();

            System.Type profileType = typeof(AutomapperConfigurationProfile);
            System.Reflection.Assembly assembly = profileType.Assembly;
            services.AddAutoMapper(new[] { assembly });
            services.AddMvc(opts => opts.EnableEndpointRouting = false);
            services.AddSwaggerDocument();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseOpenApi();
            
            app.UseSwaggerUi3();

            app.UseMvc();
        }
    }
}

