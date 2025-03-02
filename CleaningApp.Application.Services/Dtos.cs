namespace CleaningApp.Application.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class RoomDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class TaskTypeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class CleaningTaskDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid RoomId { get; set; }
    public Guid TaskTypeId { get; set; }
    public DateTime TaskDate { get; set; } = DateTime.UtcNow;
}

public class CleaningTaskViewModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string RoomName { get; set; } = string.Empty;
    public string TaskTypeName { get; set; } = string.Empty;
    public DateTime TaskDate { get; set; } = DateTime.UtcNow;
    public CleaningApp.Domain.Entities.TaskStatus Status { get; set; }
}