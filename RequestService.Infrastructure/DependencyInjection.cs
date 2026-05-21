using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RequestService.Infrastructure.Data;

namespace RequestService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services.AddDbContext<AppDbContext>(options =>
            {
                _ = options.UseNpgsql(configuration.GetConnectionString(nameof(AppDbContext)));
            });
            return services;
        }
    }
}
