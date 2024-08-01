using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exploria.CodeChallenge.Weather.Application.Provider.Meteorology;

public class MeteorologyInquiryResponse
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double GenerationtimeMs { get; set; }
    public int UtcOffsetSeconds { get; set; }
    public string Timezone { get; set; }
    public string TimezoneAbbreviation { get; set; }
    public double Elevation { get; set; }
    public HourlyUnits HourlyUnits { get; set; }
    public HourlyData Hourly { get; set; }
};
public class HourlyUnits
{
    public string Time { get; set; }
    public string Temperature2m { get; set; }
    public string Relativehumidity2m { get; set; }
    public string Windspeed10m { get; set; }
}

public class HourlyData
{
    public List<string> Time { get; set; }
    public List<double> Temperature2m { get; set; }
    public List<int> Relativehumidity2m { get; set; }
    public List<double> Windspeed10m { get; set; }
}
