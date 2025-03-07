using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaningApp.Domain.Entities;

public class DBEntities
{
}

public enum TaskStatus
{
    Planning,
    Assigned,
    Completed
}

public class User
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    [Required] [MaxLength(100)] public string Name { get; set; } = string.Empty;
}

public class Room
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    [Required] [MaxLength(100)] public string Name { get; set; } = string.Empty;
}

public class TaskType
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    [Required] [MaxLength(100)] public string Name { get; set; } = string.Empty;
}

public class CleaningTask
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    [Required] public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    [Required] public Guid RoomId { get; set; }
    public Room Room { get; set; } = null!;

    [Required] public Guid TaskTypeId { get; set; }
    public TaskType TaskType { get; set; } = null!;

    [Required] public DateTime TaskDate { get; set; } = DateTime.UtcNow;
    public TaskStatus Status { get; set; } = TaskStatus.Planning;
}

public class TaskTemplate
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    public TaskTemplateType TaskDuration { get; set; }

    // Which day is this template for? Sunday=0, Monday=1, etc.
    public int? DayOfWeek { get; set; }

    // For monthly tasks (e.g. “the 5th of every month”):
    public int? DayOfMonth { get; set; }

    // If you want a default assigned user (e.g. Planerad)
    public Guid? DefaultUserId { get; set; }
    public User? DefaultUser { get; set; }

    // The normal references to Room and TaskType
    [Required] public Guid RoomId { get; set; }
    public Room Room { get; set; } = null!;

    [Required] public Guid TaskTypeId { get; set; }
    public TaskType TaskType { get; set; } = null!;

    // Optional: Additional info (e.g. "Vacuum living room", "Dust shelves", etc.)
    [MaxLength(200)] public string Notes { get; set; } = string.Empty;
}

public enum TaskTemplateType
{
    Week,
    Month,
    Quarter,
    Year
}

public enum NavigationPaths
{
    RegisterTask,
    TaskList,
    WeekPlanner,
    TaskTemplates,
    RoomsCrud
}