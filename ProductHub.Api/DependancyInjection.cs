using ProductHub.Api.Common.Errors;
using ProductHub.Api.Common.Mappings;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ProductHub.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory , ProductHubProblemDetailsFactory>();
        services.AddMappings();
        
        return services;
    }

}