using Microsoft.EntityFrameworkCore;
using Shorten.io.Domain.Repositories;
using Shorten.io.Infrastructure.Persistence.EF;
using Shorten.io.Infrastructure.Persistence.EF.Repositories;

namespace Shorten.io.Infrastructure;

public static class InfrastructureModule
{

    public static IServiceCollection AddInfrastructureModule(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<ShortenIODbContext>(options =>
        {
            options.UseInMemoryDatabase("ShortenIODb");
        });

        services.AddScoped<IShortenedUrlRepository, EFShortenedUrlRepository>();
        return services;
    }
}
