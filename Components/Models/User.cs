namespace booking_room_admin.Components.Models;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public int TotalBookings { get; set; }
    public string Status { get; set; } = "active";
    public string Initials { get; set; } = string.Empty;
    public string EmployeeId { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Role { get; set; } = "Pengguna";
    public string PhotoUrl { get; set; } = string.Empty;
}