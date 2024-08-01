using Exploria.CodeChallenge.Weather.Application;
using Exploria.CodeChallenge.Weather.Application.Feature.Meteorology;
using Exploria.CodeChallenge.Weather.Application.Provider.Meteorology;
using Exploria.CodeChallenge.Weather.Domain.Contracts;
using Exploria.CodeChallenge.Weather.Infrustructure;
using Exploria.CodeChallenge.Weather.Infrustructure.Provider;
using Exploria.CodeChallenge.Weather.Infrustructure.Provider.Contracts;
using Exploria.CodeChallenge.Weather.Persistance;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Extensions.Http;

namespace Exploria.CodeChallenge.Weather.WebApi;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this WebApplicationBuilder builder)
    {
        builder.Configuration.GetSection(MeteorologyConfig.Position).Get<MeteorologyConfig>();
        builder.Services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssemblies(typeof(AssemblyMarker).Assembly);
        });
        builder.Services.AddScoped<IMeteorologyInquiryProvider, MeteorologyInquiryProvider>();

        ArgumentNullException.ThrowIfNull(builder.Services, nameof(builder.Services));
        ArgumentNullException.ThrowIfNull(builder.Configuration, nameof(builder.Configuration));

        builder.Services.AddDbContext<DataBaseContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("Defult"));
        });

        builder.Services.AddScoped<IDbContext, UnitOfWork>();
        return builder.Services;
    }

    internal static void RegisterHTTPClient<T>(
        this WebApplicationBuilder builder,
        Action<IServiceProvider, HttpClient> configureClient)
        where T : class
    {
        builder.Services.AddHttpClient<T>(configureClient)
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy());



        IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(3, TimeSpan.FromSeconds(30));
        }
        IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
