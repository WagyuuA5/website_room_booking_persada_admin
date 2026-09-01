using System.Security.Cryptography;
using System.Text;

namespace booking_room_admin.Components.Services
{
    public enum AuthResultType
    {
        Success,
        InvalidCredentials,
        AccountInactive,
        ConnectionFailed,
        EmailNotFound
    }

    public class AuthResult
    {
        public AuthResultType Type { get; set; }
        public string? ErrorMessage { get; set; }

        public static AuthResult Ok() => new() { Type = AuthResultType.Success };
        public static AuthResult Failed(AuthResultType type, string? msg = null) => new() { Type = type, ErrorMessage = msg };
    }

    public class UserAccount
    {
        public string Email { get; set; } = "";
        public string PasswordHash { get; set; } = "";
        public string Role { get; set; } = "Admin";
        public bool IsActive { get; set; } = true;
    }

    public class AuthService
    {
        public bool IsAuthenticated { get; private set; } = false;
        public string? CurrentUserEmail { get; private set; }

        // Seeded accounts for Development/Staging environment
        private readonly List<UserAccount> _seedUsers = new()
        {
            // Akun Uji Coba V14.2: itdevv@persada.id / 12345678
            new UserAccount
            {
                Email = "itdevv@persada.id",
                PasswordHash = HashPassword("12345678"),
                Role = "Admin",
                IsActive = true
            },
            // Akun Admin Standar
            new UserAccount
            {
                Email = "admin@ptpersada.co.id",
                PasswordHash = HashPassword("Admin123!"),
                Role = "Admin",
                IsActive = true
            },
            new UserAccount
            {
                Email = "nama@ptpersada.co.id",
                PasswordHash = HashPassword("Password123!"),
                Role = "Staff",
                IsActive = true
            },
            // Akun Nonaktif untuk simulasi
            new UserAccount
            {
                Email = "inactive@ptpersada.co.id",
                PasswordHash = HashPassword("12345678"),
                Role = "Staff",
                IsActive = false
            }
        };

        private static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password + "_persada_salt"));
            return Convert.ToBase64String(bytes);
        }

        public async Task<AuthResult> AuthenticateAsync(string email, string password)
        {
            // Simulate network latency (250-350ms)
            await Task.Delay(300);

            // Skenario simulasi gagal koneksi
            if (email.Equals("offline@ptpersada.co.id", StringComparison.OrdinalIgnoreCase))
            {
                return AuthResult.Failed(AuthResultType.ConnectionFailed, "Tidak dapat terhubung ke server. Periksa koneksi internet Anda.");
            }

            var user = _seedUsers.FirstOrDefault(u => u.Email.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase));

            // Jika user tidak ditemukan
            if (user == null)
            {
                return AuthResult.Failed(AuthResultType.InvalidCredentials, "Email atau kata sandi salah.");
            }

            // Jika user tidak aktif
            if (!user.IsActive)
            {
                return AuthResult.Failed(AuthResultType.AccountInactive, "Akun Anda tidak aktif. Hubungi administrator sistem.");
            }

            // Cek password hash
            var inputHash = HashPassword(password);
            if (inputHash == user.PasswordHash)
            {
                IsAuthenticated = true;
                CurrentUserEmail = user.Email;
                return AuthResult.Ok();
            }

            return AuthResult.Failed(AuthResultType.InvalidCredentials, "Email atau kata sandi salah.");
        }

        public async Task<AuthResult> RequestPasswordResetAsync(string email)
        {
            // Simulate network delay
            await Task.Delay(350);

            if (email.Equals("offline@ptpersada.co.id", StringComparison.OrdinalIgnoreCase))
            {
                return AuthResult.Failed(AuthResultType.ConnectionFailed, "Tidak dapat terhubung ke server. Periksa koneksi internet Anda.");
            }

            var user = _seedUsers.FirstOrDefault(u => u.Email.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase));
            if (user == null)
            {
                return AuthResult.Failed(AuthResultType.EmailNotFound, "Email tidak ditemukan dalam sistem.");
            }

            return AuthResult.Ok();
        }

        public void Logout()
        {
            IsAuthenticated = false;
            CurrentUserEmail = null;
        }
    }
}
