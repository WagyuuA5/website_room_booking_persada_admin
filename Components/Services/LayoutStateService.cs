namespace booking_room_admin.Components.Services;

public class LayoutStateService
{
    public bool IsSidebarCollapsed { get; private set; }
    
    public event Action? OnChange;

    public void ToggleSidebar()
    {
        IsSidebarCollapsed = !IsSidebarCollapsed;
        OnChange?.Invoke();
    }
}
