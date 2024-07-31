using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exploria.CodeChallenge.Weather.Infrustructure.Provider.Contracts;

public class MeteorologyConfig
{
    public const string Position = "Providers:Wallet";
    public string BaseUrl { get; set; }
    public double latitude { get; set; }
    public int longitude { get; set; }
    public string hourly { get; set; }
}
