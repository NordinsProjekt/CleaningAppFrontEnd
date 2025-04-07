using CleaningApp.Infrastructure.UnitOfWork;

namespace CleaningApp.Application.Services;

public class Service<T> : IService<T> where T : class
{
    private readonly IUnitOfWork _unitOfWork;

    public Service(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _unitOfWork.Repository<T>().GetAllAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _unitOfWork.Repository<T>().GetByIdAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await _unitOfWork.Repository<T>().AddAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        _unitOfWork.Repository<T>().Update(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _unitOfWork.Repository<T>().GetByIdAsync(id);
        if (entity != null)
        {
            _unitOfWork.Repository<T>().Remove(entity);
        }
    }
}