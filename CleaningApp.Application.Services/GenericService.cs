using CleaningApp.Application.Dtos;
using CleaningApp.Domain.Entities;
using CleaningApp.Infrastructure.UnitOfWork;

namespace CleaningApp.Application.Services;

public interface IService<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
}

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

public class RoomService(IUnitOfWork unitOfWork)
{
    public async Task<IEnumerable<RoomDto>> GetAllRoomsAsync()
    {
        var rooms = await unitOfWork.Repository<Room>().GetAllAsync();
        return rooms.Select(r => new RoomDto
        {
            Id = r.Id,
            Name = r.Name
        });
    }

    public async Task<RoomDto?> GetRoomByIdAsync(Guid id)
    {
        var room = await unitOfWork.Repository<Room>().GetByIdAsync(id);
        if (room == null)
            return null;

        return new RoomDto
        {
            Id = room.Id,
            Name = room.Name
        };
    }

    public async Task AddRoomAsync(RoomDto roomDto)
    {
        var room = new Room
        {
            Id = Guid.NewGuid(),
            Name = roomDto.Name
        };

        await unitOfWork.Repository<Room>().AddAsync(room);
        await unitOfWork.CompleteAsync();
    }

    public async Task UpdateRoomAsync(RoomDto roomDto)
    {
        var room = await unitOfWork.Repository<Room>().GetByIdAsync(roomDto.Id);
        if (room != null)
        {
            room.Name = roomDto.Name;
            unitOfWork.Repository<Room>().Update(room);
            await unitOfWork.CompleteAsync();
        }
    }

    public async Task DeleteRoomAsync(Guid id)
    {
        var room = await unitOfWork.Repository<Room>().GetByIdAsync(id);
        if (room != null)
        {
            unitOfWork.Repository<Room>().Remove(room);
            await unitOfWork.CompleteAsync();
        }
    }
}