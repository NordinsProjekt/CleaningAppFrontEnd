using System.ComponentModel.DataAnnotations;

namespace CleaningApp.Domain.Entities;

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