using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebScheluder.DAL;
using WebScheduler.Domain.Interfaces;
using WebScheduler.BLL.Mapping;
using System.Reflection;
using WebScheduler.BLL;
using WebScheduler.BLL.Interfaces;
using WebScheduler.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace WebScheduler
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(typeof(IUserService).Assembly));

            });

            services.AddApplication();
            services.AddPersistence(Configuration);
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
           

            services.AddCors(options =>
            {
                 options.AddPolicy(name: "MyCors",
                      builder =>
                      {
                          builder
                          .WithOrigins("http://localhost:3000","http://localhost:8080", "http://192.168.0.108:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomExceptionHandler();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors("MyCors");

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
