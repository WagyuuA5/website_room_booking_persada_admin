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

    private string _adminAvatarUrl = "";
    public string AdminAvatarUrl
    {
        get => _adminAvatarUrl;
        set => SetAdminAvatarUrl(value);
    }

    private string _adminFullName = "Admin Wahyu";
    public string AdminFullName
    {
        get => _adminFullName;
        set => SetAdminFullName(value);
    }

    private string _firstDayOfWeek = "Senin";
    public string FirstDayOfWeek
    {
        get => _firstDayOfWeek;
        set => SetFirstDayOfWeek(value);
    }

    public event Action? OnChange;

    public async Task InitStateAsync()
    {
        try
        {
            var storedTheme = await _js.InvokeAsync<string?>("localStorage.getItem", "persada-theme");
            if (string.IsNullOrEmpty(storedTheme))
            {
                storedTheme = await _js.InvokeAsync<string?>("localStorage.getItem", "theme-mode");
            }
            IsDarkMode = storedTheme == "dark";

            var storedSidebar = await _js.InvokeAsync<string?>("localStorage.getItem", "sidebar-collapsed");
            if (storedSidebar == "true")
            {
                IsSidebarCollapsed = true;
            }

            var storedAvatar = await _js.InvokeAsync<string?>("localStorage.getItem", "admin-avatar");
            if (!string.IsNullOrEmpty(storedAvatar))
            {
                _adminAvatarUrl = storedAvatar;
            }

            var storedName = await _js.InvokeAsync<string?>("localStorage.getItem", "admin-name");
            if (!string.IsNullOrEmpty(storedName))
            {
                _adminFullName = storedName;
            }

            var storedFirstDay = await _js.InvokeAsync<string?>("localStorage.getItem", "first-day-of-week");
            if (!string.IsNullOrEmpty(storedFirstDay))
            {
                _firstDayOfWeek = storedFirstDay;
            }

            Notify();
        }
        catch
        {
            // Ignore JS errors during prerender
        }
    }

    public async Task InitThemeAsync()
    {
        await InitStateAsync();
    }

    public void ToggleSidebar()
    {
        IsSidebarCollapsed = !IsSidebarCollapsed;
        Notify();
        try
        {
            _js.InvokeVoidAsync("localStorage.setItem", "sidebar-collapsed", IsSidebarCollapsed ? "true" : "false");
        }
        catch
        {
            // Ignore JS errors during prerender
        }
    }

    public void SetSidebarCollapsed(bool collapsed)
    {
        if (IsSidebarCollapsed != collapsed)
        {
            IsSidebarCollapsed = collapsed;
            Notify();
            try
            {
                _js.InvokeVoidAsync("localStorage.setItem", "sidebar-collapsed", IsSidebarCollapsed ? "true" : "false");
            }
            catch
            {
                // Ignore JS errors during prerender
            }
        }
    }

    public void ToggleTheme()
    {
        IsDarkMode = !IsDarkMode;
        Notify();
        try
        {
            _js.InvokeVoidAsync("localStorage.setItem", "persada-theme", IsDarkMode ? "dark" : "light");
            _js.InvokeVoidAsync("localStorage.setItem", "theme-mode", IsDarkMode ? "dark" : "light");
            _js.InvokeVoidAsync("applyThemeMode", IsDarkMode);
        }
        catch
        {
            // Ignore JS errors during prerender
        }
    }

    public void SetAdminAvatarUrl(string url)
    {
        if (_adminAvatarUrl != url)
        {
            _adminAvatarUrl = url;
            Notify();
            try
            {
                _js.InvokeVoidAsync("localStorage.setItem", "admin-avatar", url);
            }
            catch { }
        }
    }

    public void SetAdminFullName(string name)
    {
        if (_adminFullName != name)
        {
            _adminFullName = name;
            Notify();
            try
            {
                _js.InvokeVoidAsync("localStorage.setItem", "admin-name", name);
            }
            catch { }
        }
    }

    public void SetFirstDayOfWeek(string day)
    {
        if (_firstDayOfWeek != day)
        {
            _firstDayOfWeek = day;
            Notify();
            try
            {
                _js.InvokeVoidAsync("localStorage.setItem", "first-day-of-week", day);
            }
            catch { }
        }
    }

    private void Notify() => OnChange?.Invoke();
}
