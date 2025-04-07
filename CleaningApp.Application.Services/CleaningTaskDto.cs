namespace CleaningApp.Application.Services;

public class CleaningTaskDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid RoomId { get; set; }
    public Guid TaskTypeId { get; set; }
    public DateTime TaskDate { get; set; } = DateTime.UtcNow;
}