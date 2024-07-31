using Exploria.CodeChallenge.Weather.Infrustructure.Provider.Contracts;
using Polly;
using Polly.Extensions.Http;

namespace Exploria.CodeChallenge.Weather.Infrustructure;

public static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddApplication(this WebApplicationBuilder builder)
    {
        builder.Configuration.GetSection(MeteorologyConfig.Position).Get<MeteorologyConfig>();
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
