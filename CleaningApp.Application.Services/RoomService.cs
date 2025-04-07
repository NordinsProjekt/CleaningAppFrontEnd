using CleaningApp.Domain.Entities;
using CleaningApp.Infrastructure.UnitOfWork;

namespace CleaningApp.Application.Services;

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