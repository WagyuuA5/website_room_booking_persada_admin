namespace booking_room_admin.Components.Models;

public class FacilityRequest
{
    public int Id { get; set; }
    public string RequestId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string RequesterName { get; set; } = string.Empty;
    public string RequesterInitials { get; set; } = string.Empty;
    public string RequesterRole { get; set; } = string.Empty;
    public string RequesterEmail { get; set; } = string.Empty;
    public string RequesterDepartment { get; set; } = string.Empty;
    public string RoomName { get; set; } = string.Empty;
    public DateTime BookingDate { get; set; }
    public string Status { get; set; } = "pending";
    public string? AssignedTo { get; set; }
    public string? InternalNotes { get; set; }
    public string? RequesterNote { get; set; }
    public List<RequestedItem> Items { get; set; } = new();
    public List<ActivityLogEntry> ActivityLog { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

public class RequestedItem
{
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; } = 1;
    public string Category { get; set; } = string.Empty;
    public string Icon { get; set; } = "📦";
}

public class ActivityLogEntry
{
    public DateTime Timestamp { get; set; }
    public string Action { get; set; } = string.Empty;
    public string? Actor { get; set; }
}
