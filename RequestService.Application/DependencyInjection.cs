using Microsoft.Extensions.DependencyInjection;
using RequestService.Application.Services;

namespace RequestService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            _ = services.AddScoped<IDocumentRequestService, DocumentRequestService>();
            return services;
        }
    }
}
