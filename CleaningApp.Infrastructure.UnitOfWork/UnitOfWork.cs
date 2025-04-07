using Microsoft.EntityFrameworkCore;

namespace CleaningApp.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly IDbContextFactory<CleaningDBContext> _contextFactory;
    private CleaningDBContext _context;
    private readonly Dictionary<Type, object> _repositories = new();

    public UnitOfWork(IDbContextFactory<CleaningDBContext> contextFactory)
    {
        _contextFactory = contextFactory;
        _context = _contextFactory.CreateDbContext();
    }

    public IRepository<T> Repository<T>() where T : class
    {
        if (!_repositories.ContainsKey(typeof(T)))
        {
            _repositories[typeof(T)] = new GenericRepository<T>(_contextFactory);
        }

        return (IRepository<T>)_repositories[typeof(T)];
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}