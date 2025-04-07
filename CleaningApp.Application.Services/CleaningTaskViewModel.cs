namespace CleaningApp.Application.Services;

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