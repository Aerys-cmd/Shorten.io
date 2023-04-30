using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shorten.io.Application.Common;
using System.Reflection;

namespace Shorten.io.Application;

public static class ApplicationModule
{

    public static IServiceCollection AddApplicationModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        });
        services.AddValidatorsFromAssembly(typeof(ApplicationModule).Assembly);

        return services;
    }
}
