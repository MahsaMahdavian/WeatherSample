using Exploria.CodeChallenge.Weather.Domain.Contracts;
using Exploria.CodeChallenge.Weather.Domain.Entities;
using Exploria.CodeChallenge.Weather.Infrustructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exploria.CodeChallenge.Weather.Persistance;

public sealed class UnitOfWork : IDbContext
{
    private readonly DataBaseContext _dbContext;

    public UnitOfWork(DataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IMeteorologyRepository meteorologyRepository => _dbContext.ChangeTracker.Context.GetService<IMeteorologyRepository>();
    public Task BeginTransactionAsync(CancellationToken cancellationToken)
    {
        return _dbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public Task RollbackAsync(CancellationToken cancellationToken)
    {
        return _dbContext.Database.RollbackTransactionAsync(cancellationToken);
    }

    public Task CommitAsync(CancellationToken cancellationToken)
    {
        return _dbContext.Database.CommitTransactionAsync(cancellationToken);
    }

    public Task SaveAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddAsync(WeatherData entity, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(entity, cancellationToken);
    }
    public async Task<WeatherData> GetAsync(CancellationToken cancellationToken)
    {
        return  _dbContext.Weathers
            .OrderByDescending(e => e.Id)
            .FirstOrDefault();                      
    }
}