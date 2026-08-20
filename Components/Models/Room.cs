namespace booking_room_admin.Components.Models;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string Floor { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Status { get; set; } = "active";
    public int TotalBookings { get; set; }
    public List<string> Facilities { get; set; } = new();
    public DateTime? MaintenanceEnd { get; set; }
}