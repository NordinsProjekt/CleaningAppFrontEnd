using CleaningApp.Application.Dtos;
using CleaningApp.Application.Services;
using CleaningApp.Domain.Entities;
using CleaningApp.Infrastructure.UnitOfWork;
using NSubstitute;
using TaskStatus = CleaningApp.Domain.Entities.TaskStatus;

namespace CleaningApp.Application.Service.Tests;

public class ServiceUnittests
{
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IRepository<CleaningTask> _taskRepositoryMock;
    private readonly IRepository<User> _userRepositoryMock;
    private readonly IRepository<Room> _roomRepositoryMock;
    private readonly IRepository<TaskType> _taskTypeRepositoryMock;
    private readonly IRepository<TaskTemplate> _taskTemplateRepositoryMock;
    private readonly TaskService _taskService;

    public ServiceUnittests()
    {
        // Create mocks
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _taskRepositoryMock = Substitute.For<IRepository<CleaningTask>>();
        _userRepositoryMock = Substitute.For<IRepository<User>>();
        _roomRepositoryMock = Substitute.For<IRepository<Room>>();
        _taskTypeRepositoryMock = Substitute.For<IRepository<TaskType>>();
        _taskTemplateRepositoryMock = Substitute.For<IRepository<TaskTemplate>>();

        // By default, whenever TaskService calls .Repository<CleaningTask>(), return our mock
        _unitOfWorkMock.Repository<CleaningTask>().Returns(_taskRepositoryMock);
        _unitOfWorkMock.Repository<User>().Returns(_userRepositoryMock);
        _unitOfWorkMock.Repository<Room>().Returns(_roomRepositoryMock);
        _unitOfWorkMock.Repository<TaskType>().Returns(_taskTypeRepositoryMock);
        _unitOfWorkMock.Repository<TaskTemplate>().Returns(_taskTemplateRepositoryMock);

        // Create the service under test
        _taskService = new TaskService(_unitOfWorkMock);
    }

    [Fact]
    public async Task GetAllTasksAsync_ShouldReturnTasks()
    {
        // Arrange
        var cleaningTask1 = new CleaningTask
        {
            Id = Guid.NewGuid(),
            User = new User { Name = "User1" },
            Room = new Room { Name = "Room1" },
            TaskType = new TaskType { Name = "Type1" },
            TaskDate = new DateTime(2025, 1, 1)
        };
        var cleaningTask2 = new CleaningTask
        {
            Id = Guid.NewGuid(),
            User = new User { Name = "User2" },
            Room = new Room { Name = "Room2" },
            TaskType = new TaskType { Name = "Type2" },
            TaskDate = new DateTime(2025, 1, 2)
        };

        _taskRepositoryMock
            .GetAllAsync(Arg.Any<System.Linq.Expressions.Expression<Func<CleaningTask, object>>[]>())
            .Returns(new[] { cleaningTask1, cleaningTask2 });

        // Act
        var result = await _taskService.GetAllTasksAsync();

        // Assert
        var list = result.ToList();
        Assert.Equal(2, list.Count);
        Assert.Equal("User1", list[0].UserName);
        Assert.Equal("Room1", list[0].RoomName);
        Assert.Equal("Type1", list[0].TaskTypeName);

        Assert.Equal("User2", list[1].UserName);
        Assert.Equal("Room2", list[1].RoomName);
        Assert.Equal("Type2", list[1].TaskTypeName);
    }

    [Fact]
    public async Task GetTaskByIdAsync_ShouldReturnTaskDto_WhenTaskExists()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var cleaningTask = new CleaningTask
        {
            Id = taskId,
            UserId = Guid.NewGuid(),
            RoomId = Guid.NewGuid(),
            TaskTypeId = Guid.NewGuid(),
            TaskDate = new DateTime(2025, 1, 10)
        };

        _taskRepositoryMock.GetByIdAsync(taskId).Returns(cleaningTask);

        // Act
        var result = await _taskService.GetTaskByIdAsync(taskId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(taskId, result.Id);
    }

    [Fact]
    public async Task GetTaskByIdAsync_ShouldReturnNull_WhenTaskDoesNotExist()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        _taskRepositoryMock.GetByIdAsync(taskId).Returns((CleaningTask)null);

        // Act
        var result = await _taskService.GetTaskByIdAsync(taskId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddTaskAsync_ShouldAddAndComplete()
    {
        // Arrange
        var dto = new CleaningTaskDto
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            RoomId = Guid.NewGuid(),
            TaskTypeId = Guid.NewGuid(),
            TaskDate = new DateTime(2025, 2, 1)
        };

        // Act
        await _taskService.AddTaskAsync(dto);

        // Assert
        await _taskRepositoryMock.Received(1)
            .AddAsync(Arg.Is<CleaningTask>(x =>
                x.UserId == dto.UserId &&
                x.RoomId == dto.RoomId &&
                x.TaskTypeId == dto.TaskTypeId &&
                x.TaskDate == dto.TaskDate));
        await _unitOfWorkMock.Received(1).CompleteAsync();
    }

    [Fact]
    public async Task UpdateTaskAsync_ShouldUpdateAndComplete_WhenTaskExists()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var existingTask = new CleaningTask
        {
            Id = taskId,
            UserId = Guid.NewGuid(),
            RoomId = Guid.NewGuid(),
            TaskTypeId = Guid.NewGuid(),
            TaskDate = new DateTime(2025, 1, 1)
        };
        var dto = new CleaningTaskDto
        {
            Id = taskId,
            UserId = Guid.NewGuid(),
            RoomId = Guid.NewGuid(),
            TaskTypeId = Guid.NewGuid(),
            TaskDate = new DateTime(2025, 3, 1)
        };

        _taskRepositoryMock.GetByIdAsync(taskId).Returns(existingTask);

        // Act
        await _taskService.UpdateTaskAsync(dto);

        // Assert
        Assert.Equal(dto.UserId, existingTask.UserId);
        Assert.Equal(dto.RoomId, existingTask.RoomId);
        Assert.Equal(dto.TaskTypeId, existingTask.TaskTypeId);
        Assert.Equal(dto.TaskDate, existingTask.TaskDate);

        _taskRepositoryMock.Received(1).Update(existingTask);
        await _unitOfWorkMock.Received(1).CompleteAsync();
    }

    [Fact]
    public async Task UpdateTaskAsync_ShouldDoNothing_WhenTaskNotFound()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        _taskRepositoryMock.GetByIdAsync(taskId).Returns((CleaningTask)null);

        var dto = new CleaningTaskDto { Id = taskId };

        // Act
        await _taskService.UpdateTaskAsync(dto);

        // Assert
        _taskRepositoryMock.DidNotReceive().Update(Arg.Any<CleaningTask>());
        await _unitOfWorkMock.Received(0).CompleteAsync();
    }

    [Fact]
    public async Task DeleteTaskAsync_ShouldRemoveAndComplete_WhenTaskExists()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var existingTask = new CleaningTask { Id = taskId };

        _taskRepositoryMock.GetByIdAsync(taskId).Returns(existingTask);

        // Act
        await _taskService.DeleteTaskAsync(taskId);

        // Assert
        _taskRepositoryMock.Received(1).Remove(existingTask);
        await _unitOfWorkMock.Received(1).CompleteAsync();
    }

    [Fact]
    public async Task DeleteTaskAsync_ShouldDoNothing_WhenTaskNotFound()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        _taskRepositoryMock.GetByIdAsync(taskId).Returns((CleaningTask)null);

        // Act
        await _taskService.DeleteTaskAsync(taskId);

        // Assert
        _taskRepositoryMock.DidNotReceive().Remove(Arg.Any<CleaningTask>());
        await _unitOfWorkMock.Received(0).CompleteAsync();
    }

    [Fact]
    public async Task GetAllTasksForWeekAsync_ShouldReturnCorrectRange()
    {
        // Arrange
        var weekStart = new DateTime(2025, 1, 1);
        var weekEnd = weekStart.AddDays(7);

        var taskInRange = new CleaningTask
        {
            Id = Guid.NewGuid(),
            TaskDate = new DateTime(2025, 1, 3),
            User = new User { Name = "UserInRange" },
            Room = new Room { Name = "RoomInRange" },
            TaskType = new TaskType { Name = "TypeInRange" }
        };
        var taskOutOfRange = new CleaningTask
        {
            Id = Guid.NewGuid(),
            TaskDate = new DateTime(2025, 2, 10),
            User = new User { Name = "UserOutRange" },
            Room = new Room { Name = "RoomOutRange" },
            TaskType = new TaskType { Name = "TypeOutRange" }
        };

        _taskRepositoryMock.FindAsync(
            Arg.Any<System.Linq.Expressions.Expression<Func<CleaningTask, bool>>>(),
            Arg.Any<System.Linq.Expressions.Expression<Func<CleaningTask, object>>[]>()
        ).Returns(callInfo =>
        {
            var predicate = callInfo.Arg<System.Linq.Expressions.Expression<Func<CleaningTask, bool>>>();
            var tasks = new List<CleaningTask> { taskInRange, taskOutOfRange };
            return tasks.Where(predicate.Compile()).ToList();
        });

        // Act
        var result = await _taskService.GetAllTasksForWeekAsync(weekStart);

        // Assert
        var list = result.ToList();
        Assert.Single(list);
        Assert.Equal("UserInRange", list[0].UserName);
    }

    [Fact]
    public async Task AssignTaskAsync_ShouldUpdateUserAndComplete()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var existingTask = new CleaningTask { Id = taskId, UserId = Guid.NewGuid() };

        _taskRepositoryMock.GetByIdAsync(taskId).Returns(existingTask);

        // Act
        await _taskService.AssignTaskAsync(taskId, userId);

        // Assert
        Assert.Equal(userId, existingTask.UserId);
        _taskRepositoryMock.Received(1).Update(existingTask);
        await _unitOfWorkMock.Received(1).CompleteAsync();
    }

    [Fact]
    public async Task GetOrCreatePlaneradUserAsync_ShouldReturnExistingUser_IfFound()
    {
        // Arrange
        var planeradUser = new User { Id = Guid.NewGuid(), Name = "Planerad" };
        _userRepositoryMock.FindAsync(Arg.Any<System.Linq.Expressions.Expression<Func<User, bool>>>())
            .Returns(new List<User> { planeradUser });

        // Act
        var result = await _taskService.GetOrCreatePlaneradUserAsync();

        // Assert
        Assert.Equal("Planerad", result.Name);
        // Should not create a new user
        await _userRepositoryMock.DidNotReceive().AddAsync(Arg.Any<User>());
        await _unitOfWorkMock.Received(0).CompleteAsync();
    }

    [Fact]
    public async Task GetOrCreatePlaneradUserAsync_ShouldCreate_IfNotFound()
    {
        // Arrange
        _userRepositoryMock.FindAsync(Arg.Any<System.Linq.Expressions.Expression<Func<User, bool>>>())
            .Returns(new List<User>());

        // Act
        var result = await _taskService.GetOrCreatePlaneradUserAsync();

        // Assert
        Assert.Equal("Planerad", result.Name);
        await _userRepositoryMock.Received(1).AddAsync(Arg.Is<User>(u => u.Name == "Planerad"));
        await _unitOfWorkMock.Received(1).CompleteAsync();
    }

    [Fact]
    public async Task GetAllTaskTemplatesAsync_ShouldReturnTemplates()
    {
        // Arrange
        var template1 = new TaskTemplate { Id = Guid.NewGuid() };
        var template2 = new TaskTemplate { Id = Guid.NewGuid() };
        _taskTemplateRepositoryMock
            .GetAllAsync(Arg.Any<System.Linq.Expressions.Expression<Func<TaskTemplate, object>>[]>())
            .Returns(new[] { template1, template2 });

        // Act
        var result = await _taskService.GetAllTaskTemplatesAsync();

        // Assert
        var list = result.ToList();
        Assert.Equal(2, list.Count);
        Assert.Contains(list, t => t.Id == template1.Id);
        Assert.Contains(list, t => t.Id == template2.Id);
    }

    [Fact]
    public async Task AddTaskTemplateAsync_ShouldAddAndComplete()
    {
        // Arrange
        var newTemplate = new TaskTemplate { Id = Guid.NewGuid() };

        // Act
        await _taskService.AddTaskTemplateAsync(newTemplate);

        // Assert
        await _taskTemplateRepositoryMock.Received(1).AddAsync(newTemplate);
        await _unitOfWorkMock.Received(1).CompleteAsync();
    }

    [Fact]
    public async Task GetAllRoomsAsync_ShouldReturnRooms()
    {
        // Arrange
        var room1 = new Room { Id = Guid.NewGuid(), Name = "Room1" };
        var room2 = new Room { Id = Guid.NewGuid(), Name = "Room2" };
        _roomRepositoryMock.GetAllAsync().Returns(new[] { room1, room2 });

        // Act
        var rooms = await _taskService.GetAllRoomsAsync();

        // Assert
        Assert.Equal(2, rooms.Count);
        Assert.Contains(rooms, r => r.Name == "Room1");
        Assert.Contains(rooms, r => r.Name == "Room2");
    }

    [Fact]
    public async Task GetAllTaskTypesAsync_ShouldReturnTaskTypes()
    {
        // Arrange
        var type1 = new TaskType { Id = Guid.NewGuid(), Name = "Mopping" };
        var type2 = new TaskType { Id = Guid.NewGuid(), Name = "Dusting" };
        _taskTypeRepositoryMock.GetAllAsync().Returns(new[] { type1, type2 });

        // Act
        var result = await _taskService.GetAllTaskTypesAsync();

        // Assert
        var list = result.ToList();
        Assert.Equal(2, list.Count);
        Assert.Contains(list, t => t.Name == "Mopping");
        Assert.Contains(list, t => t.Name == "Dusting");
    }

    [Fact]
    public async Task GetAllUsersAsync_ShouldReturnUsers()
    {
        // Arrange
        var user1 = new User { Id = Guid.NewGuid(), Name = "Alice" };
        var user2 = new User { Id = Guid.NewGuid(), Name = "Bob" };
        _userRepositoryMock.GetAllAsync().Returns(new[] { user1, user2 });

        // Act
        var users = await _taskService.GetAllUsersAsync();

        // Assert
        Assert.Equal(2, users.Count);
        Assert.Contains(users, u => u.Name == "Alice");
        Assert.Contains(users, u => u.Name == "Bob");
    }

    [Fact]
    public async Task GenerateTasksFromTemplatesAsync_ShouldCreateTasksIfNotExists()
    {
        // Arrange
        var startDate = new DateTime(2025, 1, 1);
        var endDate = new DateTime(2025, 1, 3); // 2 days range

        var template = new TaskTemplate
        {
            Id = Guid.NewGuid(),
            TaskDuration = TaskTemplateType.Week,
            DayOfWeek = (int)DayOfWeek.Wednesday, // 1/1/2025 is a Wednesday
            RoomId = Guid.NewGuid(),
            TaskTypeId = Guid.NewGuid(),
            DefaultUserId = Guid.NewGuid()
        };

        _taskTemplateRepositoryMock.GetAllAsync(
            Arg.Any<System.Linq.Expressions.Expression<Func<TaskTemplate, object>>[]>()
        ).Returns(new[] { template });

        // Make sure the repository returns empty for existing tasks
        _taskRepositoryMock
            .FindAsync(Arg.Any<System.Linq.Expressions.Expression<Func<CleaningTask, bool>>>())
            .Returns(new List<CleaningTask>()); // means no existing tasks

        // Act
        await _taskService.GenerateTasksFromTemplatesAsync(startDate, endDate);

        // Assert
        // We expect one new CleaningTask to be created for Wed, Jan 1 2025
        await _taskRepositoryMock.Received(1).AddAsync(Arg.Is<CleaningTask>(
            t => t.TaskDate.Date == new DateTime(2025, 1, 1) &&
                 t.RoomId == template.RoomId &&
                 t.TaskTypeId == template.TaskTypeId &&
                 t.UserId == template.DefaultUserId &&
                 t.Status == TaskStatus.Planning
        ));
        await _unitOfWorkMock.Received(1).CompleteAsync();
    }

    [Fact]
    public async Task ChangeUserAsync_ShouldUpdateUser_WhenStatusIsPlanning()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var newUserId = Guid.NewGuid();
        var existingTask = new CleaningTask
        {
            Id = taskId,
            Status = TaskStatus.Planning,
            UserId = Guid.NewGuid()
        };

        _taskRepositoryMock.GetByIdAsync(taskId).Returns(existingTask);

        // Act
        await _taskService.ChangeUserAsync(taskId, newUserId);

        // Assert
        Assert.Equal(newUserId, existingTask.UserId);
        _taskRepositoryMock.Received(1).Update(existingTask);
        await _unitOfWorkMock.Received(1).CompleteAsync();
    }

    [Fact]
    public async Task ChangeUserAsync_ShouldNotUpdateUser_WhenStatusIsNotPlanning()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var newUserId = Guid.NewGuid();
        var existingTask = new CleaningTask
        {
            Id = taskId,
            Status = TaskStatus.Assigned,
            UserId = Guid.NewGuid()
        };

        _taskRepositoryMock.GetByIdAsync(taskId).Returns(existingTask);

        // Act
        await _taskService.ChangeUserAsync(taskId, newUserId);

        // Assert
        Assert.NotEqual(newUserId, existingTask.UserId);
        _taskRepositoryMock.DidNotReceive().Update(Arg.Any<CleaningTask>());
        await _unitOfWorkMock.Received(0).CompleteAsync();
    }

    [Fact]
    public async Task DeleteTaskTemplateAsync_ShouldRemoveAndComplete()
    {
        // Arrange
        var templateId = Guid.NewGuid();
        var existingTemplate = new TaskTemplate { Id = templateId };

        _taskTemplateRepositoryMock.GetByIdAsync(templateId).Returns(existingTemplate);

        // Act
        await _taskService.DeleteTaskTemplateAsync(templateId);

        // Assert
        _taskTemplateRepositoryMock.Received(1).Remove(existingTemplate);
        await _unitOfWorkMock.Received(1).CompleteAsync();
    }
}