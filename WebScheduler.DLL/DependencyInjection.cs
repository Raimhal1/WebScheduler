using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using FluentValidation;
using WebScheduler.BLL.Validation.Behaviors;
using WebScheduler.BLL.Interfaces;
using WebScheduler.BLL.Services;

namespace WebScheduler.BLL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
