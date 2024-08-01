using Exploria.CodeChallenge.Weather.Application.Exceptions;
using Exploria.CodeChallenge.Weather.Application.Provider.Meteorology;
using Exploria.CodeChallenge.Weather.Domain.Contracts;
using Exploria.CodeChallenge.Weather.Domain.Entities;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Exploria.CodeChallenge.Weather.Application.Feature.Meteorology;

public class MeteorologyInquiryHandler : IRequestHandler<MeteorologyInquiryRequestDto, MeteorologyInquiryResponseDto>
{
    private readonly IMeteorologyInquiryProvider _meteorologyInquiryProvider;
    private readonly IDbContext _dbContext;
    public MeteorologyInquiryHandler(IMeteorologyInquiryProvider meteorologyInquiryProvider,
        IDbContext dbContext)
    {
        _meteorologyInquiryProvider = meteorologyInquiryProvider;
        _dbContext = dbContext;
    }

    public async Task<MeteorologyInquiryResponseDto> Handle(MeteorologyInquiryRequestDto request, CancellationToken cancellationToken)
    {
        try
        {
            var providerResult = await _meteorologyInquiryProvider.GetWatherInfo(cancellationToken);
            await InsertToDB(providerResult, cancellationToken);
            return Mapper(providerResult);
        }
        catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
        {
           return await GetFromDB(cancellationToken);
           
        }
        catch (Exception ex)
        {

            throw;
        }      
    }

    private MeteorologyInquiryResponseDto Mapper(MeteorologyInquiryResponse inquiryResponse)
    {
        return new MeteorologyInquiryResponseDto(inquiryResponse.Latitude,
            inquiryResponse.Longitude, inquiryResponse.GenerationtimeMs,
            inquiryResponse.UtcOffsetSeconds, inquiryResponse.Timezone, inquiryResponse.TimezoneAbbreviation,
           inquiryResponse.Elevation, inquiryResponse.Hourly.Time.ToString(),
           inquiryResponse.Hourly.Temperature2m.ToString(),
           inquiryResponse.Hourly.Relativehumidity2m.ToString(),
           inquiryResponse.Hourly.Windspeed10m.ToString());
    }
    private async Task<MeteorologyInquiryResponseDto> GetFromDB(CancellationToken cancellationToken)
    {
        var weatherInfo = await _dbContext.GetAsync(cancellationToken);
        if(weatherInfo != null)
        {
            return new MeteorologyInquiryResponseDto(weatherInfo.Latitude,
           weatherInfo.Longitude, weatherInfo.GenerationTimeMs, weatherInfo.UtcOffsetSeconds,
           weatherInfo.Timezone, weatherInfo.TimezoneAbbreviation, weatherInfo.Elevation,
           JsonConvert.DeserializeObject<string>(weatherInfo.HourlyData.Time),
           JsonConvert.DeserializeObject<string>(weatherInfo.HourlyData.Temperature2M),
           JsonConvert.DeserializeObject<string>(weatherInfo.HourlyData.RelativeHumidity2M),
           JsonConvert.DeserializeObject<string>(weatherInfo.HourlyData.WindSpeed10M));
        }
        return null;

    }
    private async Task InsertToDB(MeteorologyInquiryResponse weatherResult
        , CancellationToken cancellationToken)
    {
        var hourly = new Domain.Entities.HourlyData
        {
            Time = JsonConvert.SerializeObject(weatherResult.Hourly.Time),
            Temperature2M = JsonConvert.SerializeObject(weatherResult.Hourly.Temperature2m),
            RelativeHumidity2M = JsonConvert.SerializeObject(weatherResult.Hourly.Relativehumidity2m),
            WindSpeed10M = JsonConvert.SerializeObject(weatherResult.Hourly.Windspeed10m),
        };
        var entity = new WeatherData
        {
            Latitude = weatherResult.Latitude,
            Longitude = weatherResult.Longitude,
            GenerationTimeMs = weatherResult.GenerationtimeMs,
            UtcOffsetSeconds = weatherResult.UtcOffsetSeconds,
            Timezone = weatherResult.Timezone,
            TimezoneAbbreviation = weatherResult.TimezoneAbbreviation,
            Elevation = weatherResult.Elevation,
            HourlyData = hourly
        };

        await _dbContext.AddAsync(entity, cancellationToken);
        await _dbContext.SaveAsync(cancellationToken);
    }

}
