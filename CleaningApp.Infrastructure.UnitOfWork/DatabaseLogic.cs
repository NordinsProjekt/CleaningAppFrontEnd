using Microsoft.EntityFrameworkCore;
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
    Task<int> SaveChangesAsync();
}

public class GenericRepository<T>(CleaningDBContext context) : IRepository<T>
    where T : class
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query =
            includes.Aggregate<Expression<Func<T, object>>?, IQueryable<T>>(_dbSet,
                (current, include) => current.Include(include));

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(
        Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query =
            includes.Aggregate<Expression<Func<T, object>>, IQueryable<T>>(_dbSet,
                (current, include) => current.Include(include));

        // Apply each include expression

        // Finally, apply the filter
        return await query.Where(predicate).ToListAsync();
    }
}

public interface IUnitOfWork
{
    IRepository<T> Repository<T>() where T : class;
    Task<int> CompleteAsync();
}

public class UnitOfWork(CleaningDBContext context) : IUnitOfWork
{
    private readonly Dictionary<Type, object> _repositories = new();

    public IRepository<T> Repository<T>() where T : class
    {
        if (!_repositories.ContainsKey(typeof(T)))
        {
            _repositories[typeof(T)] = new GenericRepository<T>(context);
        }

        return (IRepository<T>)_repositories[typeof(T)];
    }

    public async Task<int> CompleteAsync()
    {
        return await context.SaveChangesAsync();
    }
}