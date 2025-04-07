namespace CleaningApp.Infrastructure.UnitOfWork;

public interface IUnitOfWork
{
    IRepository<T> Repository<T>() where T : class;
    Task<int> CompleteAsync();
}