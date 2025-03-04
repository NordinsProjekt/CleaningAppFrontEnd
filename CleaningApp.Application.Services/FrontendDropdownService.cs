using CleaningApp.Application.Dtos;
using CleaningApp.Domain.Entities;
using CleaningApp.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaningApp.Application.Services;

public class FrontendDropdownService(IUnitOfWork unitOfWork)
{
    public async Task<List<RoomDto>> GetAllRoomsAsync()
    {
        var rooms = await unitOfWork.Repository<Room>().GetAllAsync();
        return rooms.Select(r => new RoomDto
        {
            Id = r.Id,
            Name = r.Name
        }).ToList();
    }

    public async Task<List<TaskTypeDto>> GetAllTaskTypesAsync()
    {
        var types = await unitOfWork.Repository<TaskType>().GetAllAsync();
        return types.Select(t => new TaskTypeDto
        {
            Id = t.Id,
            Name = t.Name
        }).ToList();
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await unitOfWork.Repository<User>().GetAllAsync();
        return users.Select(u => new UserDto
        {
            Id = u.Id,
            Name = u.Name
        }).ToList();
    }

    public async Task<IEnumerable<CleaningTaskViewModel>> GetAllTasksAsync()
    {
        var tasks = await unitOfWork.Repository<CleaningTask>()
            .GetAllAsync(t => t.User, t => t.Room, t => t.TaskType);

        return tasks.Select(t => new CleaningTaskViewModel
        {
            Id = t.Id,
            UserName = t.User.Name,
            RoomName = t.Room.Name,
            TaskTypeName = t.TaskType.Name,
            Status = t.Status,
            TaskDate = t.TaskDate
        });
    }
}