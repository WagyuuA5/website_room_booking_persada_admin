namespace booking_room_admin.Components.Models;

public class StatItem
{
    public string Label { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Trend { get; set; } = string.Empty;
    public bool? IsPositiveTrend { get; set; }
}