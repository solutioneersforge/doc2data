using DocToData.Domain.Enum;
using DocToData.Domain.Factories;
using DocToData.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DocToDomain.Infrastructure.Repositories;

public class UnitOfWork(IDBContextFactory dbContextFactory) : IUnitOfWork
{
    private readonly Dictionary<Type, object> _repositories = new();
    private readonly List<DbContext> _dbContexts = new();
    private readonly IDBContextFactory _dbContextFactory = dbContextFactory;

    public IRepository<T> Repository<T>(DBContextEnum dBContextEnum) where T : class
    {
        var dbContext = _dbContextFactory.GetDbContext(dBContextEnum);

        if (!_dbContexts.Contains(dbContext))
        {
            _dbContexts.Add(dbContext);
        }

        if (!_repositories.ContainsKey(typeof(T)))
        {
            _repositories[typeof(T)] = new Repository<T>(dbContext);
        }

        return (IRepository<T>)_repositories[typeof(T)];
    }

    public int SaveChanges()
    {
        int totalChanges = 0;

        foreach (var dbContext in _dbContexts)
        {
            totalChanges += dbContext.SaveChanges();
        }

        return totalChanges;
    }

    public async Task<int> SaveChangesAsync()
    {
        int totalChanges = 0;

        foreach (var dbContext in _dbContexts)
        {
            totalChanges += await dbContext.SaveChangesAsync();
        }

        return totalChanges;
    }

    public void Dispose()
    {
        foreach (var dbContext in _dbContexts)
        {
            dbContext.Dispose();
        }

        _repositories.Clear();
        _dbContexts.Clear();
    }
}