using CleaningApp.Application.Dtos;
using CleaningApp.Domain.Entities;
using CleaningApp.Infrastructure.UnitOfWork;

namespace CleaningApp.Application.Services;

public class TaskService
{
    private readonly IUnitOfWork _unitOfWork;

    public TaskService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<TaskViewModel>> GetAllTasksAsync()
    {
        var tasks = await _unitOfWork.Repository<CleaningApp.Domain.Entities.Task>().GetAllAsync(
            t => t.User,
            t => t.Room,
            t => t.TaskType
        );
        return tasks.Select(t => new TaskViewModel
        {
            Id = t.Id,
            UserName = t.User.Name,
            RoomName = t.Room.Name,
            TaskTypeName = t.TaskType.Name,
            TaskDate = t.TaskDate
        });
    }

    public async Task<TaskDto?> GetTaskByIdAsync(Guid id)
    {
        var task = await _unitOfWork.Repository<Domain.Entities.Task>().GetByIdAsync(id);
        return task == null ? null : new TaskDto
        {
            Id = task.Id,
            UserId = task.UserId,
            RoomId = task.RoomId,
            TaskTypeId = task.TaskTypeId,
            TaskDate = task.TaskDate
        };
    }

    public async System.Threading.Tasks.Task AddTaskAsync(TaskDto taskDto)
    {
        var task = new Domain.Entities.Task
        {
            Id = Guid.NewGuid(),
            UserId = taskDto.UserId,
            RoomId = taskDto.RoomId,
            TaskTypeId = taskDto.TaskTypeId,
            TaskDate = taskDto.TaskDate
        };
        await _unitOfWork.Repository<Domain.Entities.Task>().AddAsync(task);
        await _unitOfWork.CompleteAsync();
    }

    public async System.Threading.Tasks.Task UpdateTaskAsync(TaskDto taskDto)
    {
        var task = await _unitOfWork.Repository<Domain.Entities.Task>().GetByIdAsync(taskDto.Id);
        if (task != null)
        {
            task.UserId = taskDto.UserId;
            task.RoomId = taskDto.RoomId;
            task.TaskTypeId = taskDto.TaskTypeId;
            task.TaskDate = taskDto.TaskDate;
            _unitOfWork.Repository<Domain.Entities.Task>().Update(task);
            await _unitOfWork.CompleteAsync();
        }
    }

    public async System.Threading.Tasks.Task DeleteTaskAsync(Guid id)
    {
        var task = await _unitOfWork.Repository<Domain.Entities.Task>().GetByIdAsync(id);
        if (task != null)
        {
            _unitOfWork.Repository<Domain.Entities.Task>().Remove(task);
            await _unitOfWork.CompleteAsync();
        }
    }

    public async Task<IEnumerable<TaskViewModel>> GetAllTasksForWeekAsync(DateTime weekStart)
    {
        var weekEnd = weekStart.AddDays(7);

        var tasks = await _unitOfWork
            .Repository<CleaningApp.Domain.Entities.Task>()
            .FindAsync(
                t => t.TaskDate >= weekStart && t.TaskDate < weekEnd,
                t => t.User,
                t => t.Room,
                t => t.TaskType
            );

        return tasks.Select(t => new TaskViewModel
        {
            Id = t.Id,
            UserName = t.User.Name,
            RoomName = t.Room.Name,
            TaskTypeName = t.TaskType.Name,
            TaskDate = t.TaskDate
        });
    }

    public async System.Threading.Tasks.Task AssignTaskAsync(Guid taskId, Guid userId)
    {
        var task = await _unitOfWork.Repository<Domain.Entities.Task>().GetByIdAsync(taskId);
        if (task != null)
        {
            task.UserId = userId;
            _unitOfWork.Repository<Domain.Entities.Task>().Update(task);
            await _unitOfWork.CompleteAsync();
        }
    }

    public async Task<UserDto> GetOrCreatePlaneradUserAsync()
    {
        var userRepo = _unitOfWork.Repository<Domain.Entities.User>();

        var planeradUser = (await userRepo.FindAsync(u => u.Name == "Planerad")).FirstOrDefault();
        if (planeradUser == null)
        {
            planeradUser = new Domain.Entities.User
            {
                Name = "Planerad"
            };
            await userRepo.AddAsync(planeradUser);
            await _unitOfWork.CompleteAsync();
        }

        return new UserDto
        {
            Id = planeradUser.Id,
            Name = planeradUser.Name
        };
    }

    public async Task<IEnumerable<TaskTemplate>> GetAllTaskTemplatesAsync()
    {
        // Possibly include Room + TaskType
        var templates = await _unitOfWork
            .Repository<TaskTemplate>()
            .GetAllAsync(t => t.Room, t => t.TaskType, t => t.DefaultUser!);
        return templates;
    }

    public async System.Threading.Tasks.Task AddTaskTemplateAsync(TaskTemplate template)
    {
        await _unitOfWork.Repository<TaskTemplate>().AddAsync(template);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<IEnumerable<RoomDto>> GetAllRoomsAsync()
    {
        var rooms = await _unitOfWork.Repository<Room>().GetAllAsync();
        return rooms.Select(r => new RoomDto
        {
            Id = r.Id,
            Name = r.Name
        });
    }

    // 2) Get all task types for dropdown
    public async Task<IEnumerable<TaskTypeDto>> GetAllTaskTypesAsync()
    {
        var types = await _unitOfWork.Repository<TaskType>().GetAllAsync();
        return types.Select(t => new TaskTypeDto
        {
            Id = t.Id,
            Name = t.Name
        });
    }

    // 3) Get all users for dropdown
    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _unitOfWork.Repository<User>().GetAllAsync();
        return users.Select(u => new UserDto
        {
            Id = u.Id,
            Name = u.Name
        });
    }

    public async System.Threading.Tasks.Task GenerateTasksFromTemplatesForWeekAsync(DateTime weekStart)
    {
        // 1) Fetch all templates (with includes)
        var allTemplates = await _unitOfWork.Repository<TaskTemplate>()
            .GetAllAsync(t => t.Room, t => t.TaskType, t => t.DefaultUser);

        // 2) For each day in the selected week (0..6)
        for (int i = 0; i < 7; i++)
        {
            var currentDate = weekStart.AddDays(i);
            int dayOfWeekInt = (int)currentDate.DayOfWeek;
            // Sunday=0, Monday=1, etc. (default .NET logic)

            // 3) Filter templates for that day
            var dayTemplates = allTemplates.Where(tt => tt.DayOfWeek == dayOfWeekInt);

            foreach (var template in dayTemplates)
            {
                // Check if a task with same date + room + task type already exists
                var existingTasks = await _unitOfWork
                    .Repository<Domain.Entities.Task>()
                    .FindAsync(t =>
                        t.TaskDate.Date == currentDate.Date &&
                        t.RoomId == template.RoomId &&
                        t.TaskTypeId == template.TaskTypeId
                    );

                // If none found, create a new Task
                if (!existingTasks.Any())
                {
                    var newTask = new Domain.Entities.Task
                    {
                        Id = Guid.NewGuid(),
                        // Use the DefaultUser from template, or fallback to "Planerad"
                        UserId = template.DefaultUserId ?? Guid.Empty,
                        RoomId = template.RoomId,
                        TaskTypeId = template.TaskTypeId,
                        TaskDate = currentDate
                    };
                    await _unitOfWork.Repository<Domain.Entities.Task>().AddAsync(newTask);
                }
            }
        }

        // 4) Save once after all additions
        await _unitOfWork.CompleteAsync();
    }

    public async System.Threading.Tasks.Task DeleteTaskTemplateAsync(Guid templateId)
    {
        var repo = _unitOfWork.Repository<TaskTemplate>();
        var template = await repo.GetByIdAsync(templateId);
        if (template != null)
        {
            repo.Remove(template);
            await _unitOfWork.CompleteAsync();
        }
    }


}
