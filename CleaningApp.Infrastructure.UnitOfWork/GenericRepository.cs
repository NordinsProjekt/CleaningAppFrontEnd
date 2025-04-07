using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace CleaningApp.Infrastructure.UnitOfWork;

public class GenericRepository<T> : IRepository<T> where T : class
{
    private readonly IDbContextFactory<CleaningDBContext> _contextFactory;

    public GenericRepository(IDbContextFactory<CleaningDBContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        await using var context = _contextFactory.CreateDbContext();
        return await context.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        await using var context = _contextFactory.CreateDbContext();
        IQueryable<T> query = context.Set<T>();

        query = includes.Aggregate(query, (current, include) => current.Include(include));

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includes)
    {
        await using var context = _contextFactory.CreateDbContext();
        IQueryable<T> query = context.Set<T>();

        query = includes.Aggregate(query, (current, include) => current.Include(include));

        return await query.Where(predicate).ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await using var context = _contextFactory.CreateDbContext();
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