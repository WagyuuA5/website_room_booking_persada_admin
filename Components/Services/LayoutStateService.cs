using Microsoft.JSInterop;

namespace booking_room_admin.Components.Services;

public class LayoutStateService
{
    private readonly IJSRuntime _js;

    public LayoutStateService(IJSRuntime js)
    {
        _js = js;
    }

    public bool IsSidebarCollapsed { get; private set; }

    public bool IsDarkMode { get; private set; }

    public event Action? OnChange;

    public async Task InitThemeAsync()
    {
        try
        {
            var stored = await _js.InvokeAsync<string?>("localStorage.getItem", "theme-mode");
            IsDarkMode = stored == "dark";
            Notify();
        }
        catch
        {
            // Ignore JS errors during prerender
        }
    }

    public void ToggleSidebar()
    {
        IsSidebarCollapsed = !IsSidebarCollapsed;
        Notify();
    }

    public void ToggleTheme()
    {
        IsDarkMode = !IsDarkMode;
        Notify();
        try
        {
            _js.InvokeVoidAsync("localStorage.setItem", "theme-mode", IsDarkMode ? "dark" : "light");
            _js.InvokeVoidAsync("applyThemeMode", IsDarkMode);
        }
        catch
        {
            // Ignore JS errors during prerender
        }
    }

    private void Notify() => OnChange?.Invoke();
}
