using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exploria.CodeChallenge.Weather.Application.Feature.Meteorology;

public record  MeteorologyInquiryResponseDto(double Latitude,
    double Longitude,
    double GenerationTimeMs,
    int UtcOffsetSeconds,
    string Timezone,
    string TimezoneAbbreviation,
    double Elevation,
    string Time,
    string Temperature2M,
    string RelativeHumidity2M,
    string WindSpeed10M);
