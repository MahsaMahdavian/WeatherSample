using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exploria.CodeChallenge.Weather.Domain.Entities;

public class WeatherData
{
    public int Id { get; set; } // Primary Key
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double GenerationTimeMs { get; set; }
    public int UtcOffsetSeconds { get; set; }
    public string Timezone { get; set; }
    public string TimezoneAbbreviation { get; set; }
    public double Elevation { get; set; }

    // Navigation property
    public HourlyData HourlyData { get; set; }
}
