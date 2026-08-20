namespace booking_room_admin.Components.Models;

public class Booking
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Room { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string StartTime { get; set; } = string.Empty;
    public string EndTime { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public string RequesterName { get; set; } = string.Empty;
    public string RequesterDivision { get; set; } = string.Empty;
    public string RequesterInitials { get; set; } = string.Empty;
    public int ParticipantsCount { get; set; }
    public string Status { get; set; } = string.Empty;
    public bool IsRecurring { get; set; }
    public string AccentColor { get; set; } = "warning";
}