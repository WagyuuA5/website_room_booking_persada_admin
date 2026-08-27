namespace booking_room_admin.Components.Models;

public class AuditLogEntry
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public string ActorName { get; set; } = string.Empty;
    public string ActorInitials { get; set; } = string.Empty;
    public string ActionType { get; set; } = string.Empty;
    public string ActionCategory { get; set; } = string.Empty;
    public string TargetName { get; set; } = string.Empty;
    public string Detail { get; set; } = string.Empty;
}
