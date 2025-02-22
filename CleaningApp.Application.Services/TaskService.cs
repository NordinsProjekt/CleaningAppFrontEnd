using CleaningApp.Application.Dtos;
using CleaningApp.Domain.Entities;
using CleaningApp.Infrastructure.UnitOfWork;

namespace CleaningApp.Application.Services;

public class TaskService(IUnitOfWork unitOfWork)
{
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
            TaskDate = t.TaskDate
        });
    }

    public async Task<CleaningTaskDto?> GetTaskByIdAsync(Guid id)
    {
        var task = await unitOfWork.Repository<CleaningTask>().GetByIdAsync(id);
        return task == null
            ? null
            : new CleaningTaskDto
            {
                Id = task.Id,
                UserId = task.UserId,
                RoomId = task.RoomId,
                TaskTypeId = task.TaskTypeId,
                TaskDate = task.TaskDate
            };
    }

    public async Task AddTaskAsync(CleaningTaskDto taskDto)
    {
        var task = new CleaningTask
        {
            Id = Guid.NewGuid(),
            UserId = taskDto.UserId,
            RoomId = taskDto.RoomId,
            TaskTypeId = taskDto.TaskTypeId,
            TaskDate = taskDto.TaskDate
        };
        await unitOfWork.Repository<CleaningTask>().AddAsync(task);
        await unitOfWork.CompleteAsync();
    }

    public async Task UpdateTaskAsync(CleaningTaskDto taskDto)
    {
        var task = await unitOfWork.Repository<CleaningTask>().GetByIdAsync(taskDto.Id);
        if (task != null)
        {
            task.UserId = taskDto.UserId;
            task.RoomId = taskDto.RoomId;
            task.TaskTypeId = taskDto.TaskTypeId;
            task.TaskDate = taskDto.TaskDate;
            unitOfWork.Repository<CleaningTask>().Update(task);
            await unitOfWork.CompleteAsync();
        }
    }

    public async Task DeleteTaskAsync(Guid id)
    {
        var task = await unitOfWork.Repository<CleaningTask>().GetByIdAsync(id);
        if (task != null)
        {
            unitOfWork.Repository<CleaningTask>().Remove(task);
            await unitOfWork.CompleteAsync();
        }
    }

    public async Task<IEnumerable<CleaningTaskViewModel>> GetAllTasksForWeekAsync(DateTime weekStart)
    {
        var weekEnd = weekStart.AddDays(7);

        var tasks = await unitOfWork
            .Repository<CleaningTask>()
            .FindAsync(
                t => t.TaskDate >= weekStart && t.TaskDate < weekEnd,
                t => t.User,
                t => t.Room,
                t => t.TaskType
            );

        return tasks.Select(t => new CleaningTaskViewModel
        {
            Id = t.Id,
            UserName = t.User.Name,
            RoomName = t.Room.Name,
            TaskTypeName = t.TaskType.Name,
            TaskDate = t.TaskDate
        });
    }

    public async Task AssignTaskAsync(Guid taskId, Guid userId)
    {
        var task = await unitOfWork.Repository<CleaningTask>().GetByIdAsync(taskId);
        if (task != null)
        {
            task.UserId = userId;
            unitOfWork.Repository<CleaningTask>().Update(task);
            await unitOfWork.CompleteAsync();
        }
    }

    public async Task<UserDto> GetOrCreatePlaneradUserAsync()
    {
        var userRepo = unitOfWork.Repository<User>();

        var planeradUser = (await userRepo.FindAsync(u => u.Name == "Planerad")).FirstOrDefault();
        if (planeradUser == null)
        {
            planeradUser = new User
            {
                Name = "Planerad"
            };
            await userRepo.AddAsync(planeradUser);
            await unitOfWork.CompleteAsync();
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
        var templates = await unitOfWork
            .Repository<TaskTemplate>()
            .GetAllAsync(t => t.Room, t => t.TaskType, t => t.DefaultUser!);
        return templates;
    }

    public async Task AddTaskTemplateAsync(TaskTemplate template)
    {
        await unitOfWork.Repository<TaskTemplate>().AddAsync(template);
        await unitOfWork.CompleteAsync();
    }

    public async Task<List<RoomDto>> GetAllRoomsAsync()
    {
        var rooms = await unitOfWork.Repository<Room>().GetAllAsync();
        return rooms.Select(r => new RoomDto
        {
            Id = r.Id,
            Name = r.Name
        }).ToList();
    }

    // 2) Get all task types for dropdown
    public async Task<List<TaskTypeDto>> GetAllTaskTypesAsync()
    {
        var types = await unitOfWork.Repository<TaskType>().GetAllAsync();
        return types.Select(t => new TaskTypeDto
        {
            Id = t.Id,
            Name = t.Name
        }).ToList();
    }

    // 3) Get all users for dropdown
    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await unitOfWork.Repository<User>().GetAllAsync();
        return users.Select(u => new UserDto
        {
            Id = u.Id,
            Name = u.Name
        }).ToList();
    }

    public async Task GenerateTasksFromTemplatesForWeekAsync(DateTime weekStart)
    {
        // 1) Fetch all templates (with includes)
        var allTemplates = await unitOfWork.Repository<TaskTemplate>()
            .GetAllAsync(t => t.Room, t => t.TaskType, t => t.DefaultUser);

        // 2) For each day in the selected week (0..6)
        for (var i = 0; i < 7; i++)
        {
            var currentDate = weekStart.AddDays(i);
            var dayOfWeekInt = (int)currentDate.DayOfWeek;
            // Sunday=0, Monday=1, etc. (default .NET logic)

            // 3) Filter templates for that day
            var dayTemplates = allTemplates.Where(tt => tt.DayOfWeek == dayOfWeekInt);

            foreach (var template in dayTemplates)
            {
                // Check if a task with same date + room + task type already exists
                var existingTasks = await unitOfWork
                    .Repository<CleaningTask>()
                    .FindAsync(t =>
                        t.TaskDate.Date == currentDate.Date &&
                        t.RoomId == template.RoomId &&
                        t.TaskTypeId == template.TaskTypeId
                    );

                // If none found, create a new Task
                if (!existingTasks.Any())
                {
                    var newTask = new CleaningTask
                    {
                        Id = Guid.NewGuid(),
                        // Use the DefaultUser from template, or fallback to "Planerad"
                        UserId = template.DefaultUserId ?? Guid.Empty,
                        RoomId = template.RoomId,
                        TaskTypeId = template.TaskTypeId,
                        TaskDate = currentDate
                    };
                    await unitOfWork.Repository<CleaningTask>().AddAsync(newTask);
                }
            }
        }

        // 4) Save once after all additions
        await unitOfWork.CompleteAsync();
    }

    public async Task DeleteTaskTemplateAsync(Guid templateId)
    {
        var repo = unitOfWork.Repository<TaskTemplate>();
        var template = await repo.GetByIdAsync(templateId);
        if (template != null)
        {
            repo.Remove(template);
            await unitOfWork.CompleteAsync();
        }
    }
}