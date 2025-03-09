using CleaningApp.Application.Dtos;
using CleaningApp.Domain.Entities;
using CleaningApp.Infrastructure.UnitOfWork;
using TaskStatus = CleaningApp.Domain.Entities.TaskStatus;

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
            Status = t.Status,
            TaskDate = t.TaskDate
        });
    }

    public async Task<CleaningTaskDto?> GetTaskByIdAsync(Guid id)
    {
        var task = await unitOfWork.Repository<CleaningTask>().GetByIdAsync(id);
        if (task is null) return null;

        return new CleaningTaskDto
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
        var newTask = taskDto.ToEntity();

        await unitOfWork.Repository<CleaningTask>().AddAsync(newTask);
        await unitOfWork.CompleteAsync();
    }

    public async Task UpdateTaskAsync(CleaningTaskDto taskDto)
    {
        var task = await unitOfWork.Repository<CleaningTask>().GetByIdAsync(taskDto.Id);
        if (task is null) return;

        task.UpdateFromDto(taskDto);

        unitOfWork.Repository<CleaningTask>().Update(task);
        await unitOfWork.CompleteAsync();
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

        return tasks.Select(t => t.ToViewModel());
    }

    public async Task AssignTaskAsync(Guid taskId, Guid userId)
    {
        var task = await unitOfWork.Repository<CleaningTask>().GetByIdAsync(taskId);

        task.UserId = userId;
        unitOfWork.Repository<CleaningTask>().Update(task);
        await unitOfWork.CompleteAsync();
    }

    public async Task CompleteCleaningTask(Guid taskId)
    {
        var task = await unitOfWork.Repository<CleaningTask>().GetByIdAsync(taskId);
        task.Status = TaskStatus.Completed;

        unitOfWork.Repository<CleaningTask>().Update(task);

        await unitOfWork.CompleteAsync();
        var check = await unitOfWork.Repository<CleaningTask>().GetByIdAsync(taskId);
    }

    public async Task UnCompleteCleaningTask(Guid taskId)
    {
        var task = await unitOfWork.Repository<CleaningTask>().GetByIdAsync(taskId);
        task.Status = TaskStatus.Assigned;

        unitOfWork.Repository<CleaningTask>().Update(task);

        await unitOfWork.CompleteAsync();
        var check = await unitOfWork.Repository<CleaningTask>().GetByIdAsync(taskId);
    }

    public async Task<UserDto> GetOrCreatePlaneradUserAsync()
    {
        var userRepo = unitOfWork.Repository<User>();

        var planeradUser = (await userRepo.FindAsync(u => u.Name == "Planerad")).FirstOrDefault();

        if (planeradUser != null)
            return new UserDto
            {
                Id = planeradUser.Id,
                Name = planeradUser.Name
            };

        planeradUser = new User
        {
            Name = "Planerad"
        };

        await userRepo.AddAsync(planeradUser);
        await unitOfWork.CompleteAsync();

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

    public async Task GenerateTasksFromTemplatesAsync(DateTime startDate, DateTime endDate)
    {
        var allTemplates = await unitOfWork.Repository<TaskTemplate>()
            .GetAllAsync(t => t.Room, t => t.TaskType, t => t.DefaultUser);

        var currentDate = startDate.Date;
        while (currentDate < endDate)
        {
            foreach (var template in allTemplates)
            {
                switch (template.TaskDuration)
                {
                    case TaskTemplateType.Week:
                        if ((int)currentDate.DayOfWeek == template.DayOfWeek)
                        {
                            await CreateTaskIfNotExists(template, currentDate);
                        }

                        break;

                    case TaskTemplateType.Month:
                        if (currentDate.Day == template.DayOfMonth)
                        {
                            await CreateTaskIfNotExists(template, currentDate);
                        }

                        break;
                }
            }

            currentDate = currentDate.AddDays(1);
        }

        await unitOfWork.CompleteAsync();
    }

    private async Task CreateTaskIfNotExists(TaskTemplate template, DateTime date)
    {
        // Check if a task with same date, room, task type already exists
        var existingTasks = await unitOfWork.Repository<CleaningTask>().FindAsync(
            t => t.TaskDate.Date == date.Date
                 && t.RoomId == template.RoomId
                 && t.TaskTypeId == template.TaskTypeId
        );
        if (!existingTasks.Any())
        {
            var newTask = new CleaningTask
            {
                Id = Guid.NewGuid(),
                // If you store the status:
                Status = TaskStatus.Planning,
                // default user or some "Planning" user
                UserId = template.DefaultUserId ?? Guid.Empty,
                RoomId = template.RoomId,
                TaskTypeId = template.TaskTypeId,
                TaskDate = date
            };
            await unitOfWork.Repository<CleaningTask>().AddAsync(newTask);
        }
    }

    public async Task ChangeUserAsync(Guid taskId, Guid newUserId)
    {
        var task = await unitOfWork.Repository<CleaningTask>().GetByIdAsync(taskId);

        if (task.Status == TaskStatus.Planning)
        {
            task.UserId = newUserId;
            unitOfWork.Repository<CleaningTask>().Update(task);
            await unitOfWork.CompleteAsync();
        }
    }

    public async Task DeleteTaskTemplateAsync(Guid templateId)
    {
        var repo = unitOfWork.Repository<TaskTemplate>();
        var template = await repo.GetByIdAsync(templateId);

        repo.Remove(template);
        await unitOfWork.CompleteAsync();
    }

    public async Task GenerateMonthlyTasksForPlaneradAsync()
    {
        var startDate = DateTime.UtcNow.Date;
        var endDate = startDate.AddDays(30);

        // Get all task templates
        var allTemplates = await unitOfWork.Repository<TaskTemplate>()
            .GetAllAsync(t => t.Room, t => t.TaskType, t => t.DefaultUser);

        // Ensure "Planerad" user exists
        var planeradUser = await GetOrCreatePlaneradUserAsync();

        var currentDate = startDate;
        while (currentDate <= endDate)
        {
            foreach (var template in allTemplates)
            {
                var shouldCreateTask = template.TaskDuration switch
                {
                    TaskTemplateType.Week => (int)currentDate.DayOfWeek == template.DayOfWeek,
                    TaskTemplateType.Month => currentDate.Day == template.DayOfMonth,
                    _ => false
                };

                if (shouldCreateTask)
                {
                    await CreateTaskIfNotExists(template, currentDate, planeradUser.Id);
                }
            }

            currentDate = currentDate.AddDays(1);
        }

        await unitOfWork.CompleteAsync();
    }

    private async Task CreateTaskIfNotExists(TaskTemplate template, DateTime date, Guid userId)
    {
        // Check if a task already exists for this date, room, and task type
        var existingTasks = await unitOfWork.Repository<CleaningTask>().FindAsync(
            t => t.TaskDate.Date == date.Date && t.RoomId == template.RoomId && t.TaskTypeId == template.TaskTypeId);

        if (!existingTasks.Any())
        {
            var newTask = new CleaningTask
            {
                Id = Guid.NewGuid(),
                Status = TaskStatus.Planning,
                UserId = userId,
                RoomId = template.RoomId,
                TaskTypeId = template.TaskTypeId,
                TaskDate = date
            };
            await unitOfWork.Repository<CleaningTask>().AddAsync(newTask);
        }
    }
}