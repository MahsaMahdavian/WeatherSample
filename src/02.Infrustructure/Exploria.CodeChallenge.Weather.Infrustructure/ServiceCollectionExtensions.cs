using Exploria.CodeChallenge.Weather.Application.Provider.Meteorology;
using Exploria.CodeChallenge.Weather.Infrustructure.Provider;
using Exploria.CodeChallenge.Weather.Infrustructure.Provider.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exploria.CodeChallenge.Weather.Infrustructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
       IConfiguration configuration)
    {
       var aaa= configuration.GetSection(MeteorologyConfig.Position);
        services.AddScoped<IMeteorologyInquiryProvider, MeteorologyInquiryProvider>();
    
        return services;
    }

}
