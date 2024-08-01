using Exploria.CodeChallenge.Weather.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exploria.CodeChallenge.Weather.Domain.Contracts;

public interface IMeteorologyRepository
{
    Task AddAsync(WeatherData entity, CancellationToken cancellationToken);
}
