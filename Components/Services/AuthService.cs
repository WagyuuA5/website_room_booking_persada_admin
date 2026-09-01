namespace booking_room_admin.Components.Services
{
    public enum AuthResultType
    {
        Success,
        InvalidCredentials,
        AccountInactive,
        ConnectionFailed
    }

    public class AuthResult
    {
        public AuthResultType Type { get; set; }
        public string? ErrorMessage { get; set; }

        public static AuthResult Ok() => new() { Type = AuthResultType.Success };
        public static AuthResult Failed(AuthResultType type, string? msg = null) => new() { Type = type, ErrorMessage = msg };
    }

    public class AuthService
    {
        public bool IsAuthenticated { get; private set; } = false;

        public async Task<AuthResult> AuthenticateAsync(string email, string password)
        {
            // Simulate network latency
            await Task.Delay(350);

            if (email.Equals("offline@ptpersada.co.id", StringComparison.OrdinalIgnoreCase))
            {
                return AuthResult.Failed(AuthResultType.ConnectionFailed, "Tidak dapat terhubung ke server. Periksa koneksi internet Anda.");
            }

            if (email.Equals("inactive@ptpersada.co.id", StringComparison.OrdinalIgnoreCase))
            {
                return AuthResult.Failed(AuthResultType.AccountInactive, "Akun Anda tidak aktif. Hubungi administrator sistem.");
            }

            // Normal demo valid credentials
            if ((email.Equals("admin@ptpersada.co.id", StringComparison.OrdinalIgnoreCase) || email.Equals("nama@ptpersada.co.id", StringComparison.OrdinalIgnoreCase))
                && (password == "Admin123!" || password == "Password123!"))
            {
                IsAuthenticated = true;
                return AuthResult.Ok();
            }

            return AuthResult.Failed(AuthResultType.InvalidCredentials, "Email atau kata sandi salah.");
        }

        public void Logout()
        {
            IsAuthenticated = false;
        }
    }
}
