using Exploria.CodeChallenge.Weather.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exploria.CodeChallenge.Weather.Domain.Contracts;

public interface IDbContext
{
    IMeteorologyRepository meteorologyRepository { get; }
    Task AddAsync(WeatherData entity, CancellationToken cancellationToken);
    Task<WeatherData> GetAsync(CancellationToken cancellationToken);
    Task BeginTransactionAsync(CancellationToken cancellationToken);
    Task RollbackAsync(CancellationToken cancellationToken);
    Task CommitAsync(CancellationToken cancellationToken);
    Task SaveAsync(CancellationToken cancellationToken);
}
