using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using WebScheduler.Domain.Interfaces;

namespace WebScheluder.DAL
{
    public static class DependencyInjection 
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddDbContext<WebSchedulerContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DbConnection")));

            services.AddScoped<IEventDbContext>(provider =>
                provider.GetService<WebSchedulerContext>());
            services.AddScoped<IUserDbContext>(provider =>
                provider.GetService<WebSchedulerContext>());
            services.AddScoped<IRoleDbContext>(provider =>
                provider.GetService<WebSchedulerContext>());
            services.AddScoped<IFileDbContext>(provider =>
                provider.GetService<WebSchedulerContext>());
            services.AddScoped<IAllowedFileTypeDbContext>(provider =>
                provider.GetService<WebSchedulerContext>());
            services.AddScoped<IEventFileDbContext>(provider =>
                provider.GetService<WebSchedulerContext>());
            services.AddScoped<IReportDbContext>(provider =>
                provider.GetService<WebSchedulerContext>());
            return services;
        }
    }
}
