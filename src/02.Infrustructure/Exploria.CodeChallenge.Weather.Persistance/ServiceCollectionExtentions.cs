using Exploria.CodeChallenge.Weather.Domain.Contracts;
using Exploria.CodeChallenge.Weather.Infrustructure;
using Exploria.CodeChallenge.Weather.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exploria.CodeChallenge.Weather.Persistance;

public static class ServiceCollectionExtentions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

        services.AddDbContext<DataBaseContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Defult"));
        });

        services.AddScoped<IDbContext, UnitOfWork>();
        services.AddScoped<IMeteorologyRepository, MeteorologyRepository>();
        return services;
    }
}
