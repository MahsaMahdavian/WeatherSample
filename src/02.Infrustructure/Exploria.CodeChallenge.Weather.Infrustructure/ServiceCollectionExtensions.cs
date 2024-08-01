using Exploria.CodeChallenge.Weather.Application.Provider.Meteorology;
using Exploria.CodeChallenge.Weather.Infrustructure.Provider;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Exploria.CodeChallenge.Weather.Infrustructure;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
       IConfiguration configuration)
    {   
        services.AddScoped<IMeteorologyInquiryProvider, MeteorologyInquiryProvider>();   
        return services;
    }

}
