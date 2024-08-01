using Exploria.CodeChallenge.Weather.Application.Exceptions;
using Exploria.CodeChallenge.Weather.Application.Provider.Meteorology;
using Exploria.CodeChallenge.Weather.Domain.Entities;
using Exploria.CodeChallenge.Weather.Infrustructure.Provider.Contracts;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Json;

namespace Exploria.CodeChallenge.Weather.Infrustructure.Provider;

public class MeteorologyInquiryProvider : IMeteorologyInquiryProvider
{
    private readonly HttpClient _httpClient;
    private readonly MeteorologyConfig _config;

    public MeteorologyInquiryProvider(HttpClient httpClient,
        IOptions<MeteorologyConfig> config)
    {
        _httpClient = httpClient;
        _config = config.Value;
    }

    public async Task<MeteorologyInquiryResponse> GetWatherInfo(CancellationToken cancellationToken)
    {
        //TODO:Add Log
        MeteorologyInquiryResponse result = null;
        HttpResponseMessage response = null;
        try
        {
            var uri = new Uri($"{_config.BaseUrl}?latitude={_config.latitude}&longitude={_config.longitude}&hourly={_config.hourly}");
            _httpClient.Timeout = TimeSpan.FromSeconds(5);
            response = await _httpClient.GetAsync(uri, cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            response.EnsureSuccessStatusCode();
            result = await response.Content.ReadFromJsonAsync<MeteorologyInquiryResponse>(options: null, cancellationToken);
            if (result is null ||
              !response.IsSuccessStatusCode ||
                 response.StatusCode != HttpStatusCode.OK)
            {
                //_logger.LogError("Error response in GetWatherInfo service");
                throw new BusinessException(ResultCode.Failure.Code, nameof(ResultCode.Failure), ResultCode.Failure.Description);
            }
        }
        catch (Exception ex)
        {

           // _logger.LogError("Error during GetWatherInfo service{@ex}", ex);
        }
        return result;
    }
}
