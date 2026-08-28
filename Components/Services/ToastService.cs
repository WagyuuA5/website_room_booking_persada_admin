namespace booking_room_admin.Components.Services;

public class ToastService
{
    public event Action<string, string>? OnToast;

    public void Show(string message, string type = "success")
    {
        OnToast?.Invoke(message, type);
    }

    public void Success(string message) => Show(message, "success");
    public void Error(string message) => Show(message, "error");
    public void Info(string message) => Show(message, "info");
}
