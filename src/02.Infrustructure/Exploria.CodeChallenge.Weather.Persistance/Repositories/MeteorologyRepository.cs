using Exploria.CodeChallenge.Weather.Domain.Contracts;
using Exploria.CodeChallenge.Weather.Domain.Entities;
using Exploria.CodeChallenge.Weather.Infrustructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exploria.CodeChallenge.Weather.Persistance.Repositories;

public class MeteorologyRepository: IMeteorologyRepository
{
    private readonly DataBaseContext _dbContext;

    public MeteorologyRepository(DataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task AddAsync(WeatherData entity, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        return _dbContext.AddAsync(entity, cancellationToken).AsTask();
    }

    //public Task<WeatherData?> GetAsync(string specification, CancellationToken cancellationToken)
    //{
    //    return _dbContext.FindAsync(
    //}
}
