using System.ComponentModel.DataAnnotations;

namespace CleaningApp.Domain.Entities;

public class TaskTemplate
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    public TaskTemplateType TaskDuration { get; set; }

    public int? DayOfWeek { get; set; }

    public int? DayOfMonth { get; set; }

    public Guid? DefaultUserId { get; set; }
    public User? DefaultUser { get; set; }

    [Required] public Guid RoomId { get; set; }
    public Room Room { get; set; } = null!;

    [Required] public Guid TaskTypeId { get; set; }
    public TaskType TaskType { get; set; } = null!;

    [MaxLength(200)] public string Notes { get; set; } = string.Empty;
}