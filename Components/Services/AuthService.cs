namespace booking_room_admin.Components.Services
{
    public class AuthService
    {
        public bool IsAuthenticated { get; private set; } = false;

        public async Task<bool> LoginAsync(string email, string password)
        {
            // Simulate network delay
            await Task.Delay(1000);
            
            if (email == "admin@ptpersada.co.id" && password == "Admin123!")
            {
                IsAuthenticated = true;
                return true;
            }
            
            return false;
        }

        public void Logout()
        {
            IsAuthenticated = false;
        }
    }
}
