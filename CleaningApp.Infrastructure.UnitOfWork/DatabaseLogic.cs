using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace CleaningApp.Infrastructure.UnitOfWork;

public class DatabaseLogic
{
}

public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includes);

    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
}

public class GenericRepository<T> : IRepository<T> where T : class
{
    private readonly IDbContextFactory<CleaningDBContext> _contextFactory;

    public GenericRepository(IDbContextFactory<CleaningDBContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        using var context = _contextFactory.CreateDbContext();
        IQueryable<T> query = context.Set<T>();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Set<T>().Where(predicate).ToListAsync();
    }

    public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includes)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(T entity)
    {
        using var context = _contextFactory.CreateDbContext();
        await context.Set<T>().AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Set<T>().Update(entity);
        context.SaveChanges();
    }

    public void Remove(T entity)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Set<T>().Remove(entity);
        context.SaveChanges();
    }
}

public interface IUnitOfWork
{
    IRepository<T> Repository<T>() where T : class;
    Task<int> CompleteAsync();
}

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