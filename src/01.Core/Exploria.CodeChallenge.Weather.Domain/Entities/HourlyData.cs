using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exploria.CodeChallenge.Weather.Domain.Entities;

public class HourlyData
{
    public int Id { get; set; } // Primary Key
    public string Time { get; set; }
    public string Temperature2M { get; set; }
    public string RelativeHumidity2M { get; set; }
    public string WindSpeed10M { get; set; }

    // Foreign Key
    public int WeatherDataId { get; set; }
    public WeatherData WeatherData { get; set; }
}
