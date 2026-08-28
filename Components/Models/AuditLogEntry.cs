namespace booking_room_admin.Components.Models;

public class AuditLogEntry
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public string ActorName { get; set; } = string.Empty;
    public string ActorInitials { get; set; } = string.Empty;
    public string ActorBg { get; set; } = "#0B1E39";
    public string ActionType { get; set; } = string.Empty;
    public string ActionCategory { get; set; } = string.Empty;
    public string TargetName { get; set; } = string.Empty;
    public string Detail { get; set; } = string.Empty;
}
