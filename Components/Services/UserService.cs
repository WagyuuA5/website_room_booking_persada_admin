namespace booking_room_admin.Components.Services;

using booking_room_admin.Components.Models;

public interface IUserService
{
    Task<(IEnumerable<User> Users, int TotalCount)> GetUsersAsync(int page, int pageSize, string searchQuery, string roleFilter);
    Task<(IEnumerable<User> Users, int TotalCount)> GetUsersFilteredAsync(int page, int pageSize, string searchQuery, string departmentFilter, string roleFilter = "All");
    Task<List<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<bool> AddUserAsync(User user);
    Task<bool> UpdateUserAsync(User user);
    Task UpdateUserRoleAsync(int id, string newRole);
    Task UpdateUserStatusAsync(int id, string newStatus);
    Task DeleteUserAsync(int id);
    Task<int> ImportUsersAsync(IEnumerable<User> users);
}

public class UserService : IUserService
{
    private readonly List<User> _users = new();

    public UserService()
    {
        var sampleUsers = new List<(string Name, string Email, string Dept, string Role, string Status, int Bookings, string EmpId, string Phone)>
        {
            ("Budi Santoso", "budi.santoso@ptpersada.co.id", "Operasional", "Manager", "active", 12, "EMP-1001", "+62 811-2345-6789"),
            ("Sarah Jenkins", "sarah.j@ptpersada.co.id", "IT & Digital", "Administrator", "active", 28, "EMP-1002", "+62 812-3456-7890"),
            ("Ahmad Fauzi", "ahmad.f@ptpersada.co.id", "IT & Digital", "User", "active", 15, "EMP-1003", "+62 813-4567-8901"),
            ("Dewi Kartika", "dewi.k@ptpersada.co.id", "Sumber Daya Manusia (SDM)", "Approver", "active", 9, "EMP-1004", "+62 814-5678-9012"),
            ("Rizky Hidayat", "rizky.h@ptpersada.co.id", "Keuangan", "User", "inactive", 3, "EMP-1005", "+62 815-6789-0123"),
            ("Maya Putri", "maya.p@ptpersada.co.id", "Marketing & Komunikasi", "Viewer", "active", 7, "EMP-1006", "+62 816-7890-1234"),
            ("Eko Prasetyo", "eko.p@ptpersada.co.id", "General Affairs (GA)", "User", "active", 18, "EMP-1007", "+62 817-8901-2345"),
            ("Siti Nurhaliza", "siti.n@ptpersada.co.id", "Legal & Kepatuhan", "Approver", "active", 11, "EMP-1008", "+62 818-9012-3456"),
            ("Hendra Wijaya", "hendra.w@ptpersada.co.id", "Layanan Keamanan", "User", "active", 5, "EMP-1009", "+62 819-0123-4567"),
            ("Lestari Kusuma", "lestari.k@ptpersada.co.id", "Call Center & Layanan Pelanggan", "User", "inactive", 1, "EMP-1010", "+62 820-1234-5678"),
            ("Wahyu Pratama", "wahyu.p@ptpersada.co.id", "Teknikal & Pemeliharaan Telekomunikasi", "Manager", "active", 21, "EMP-1011", "+62 821-2345-6789"),
            ("Rani Anggraini", "rani.a@ptpersada.co.id", "Layanan Perkantoran", "User", "active", 8, "EMP-1012", "+62 822-3456-7890")
        };

        int id = 1;
        foreach (var u in sampleUsers)
        {
            _users.Add(new User
            {
                Id = id++,
                FullName = u.Name,
                Email = u.Email,
                Department = u.Dept,
                Role = u.Role,
                Status = u.Status,
                TotalBookings = u.Bookings,
                Initials = GetInitials(u.Name),
                EmployeeId = u.EmpId,
                Phone = u.Phone
            });
        }

        // Additional 35 realistic users for realistic pagination
        var depts = new[] { 
            "IT & Digital", 
            "Operasional", 
            "Sumber Daya Manusia (SDM)", 
            "Keuangan", 
            "Marketing & Komunikasi", 
            "Legal & Kepatuhan", 
            "General Affairs (GA)", 
            "Layanan Keamanan", 
            "Call Center & Layanan Pelanggan", 
            "Layanan Perkantoran", 
            "Teknikal & Pemeliharaan Telekomunikasi" 
        };
        var roles = new[] { "Administrator", "Manager", "User", "Viewer", "Approver" };
        var firstNames = new[] { "Andi", "Bambang", "Citra", "Dian", "Fajar", "Gita", "Hadi", "Indah", "Joko", "Kartika", "Lukman", "Mega", "Nugroho", "Putri", "Rendra" };
        var lastNames = new[] { "Saputra", "Pratama", "Wibowo", "Siregar", "Kusuma", "Utami", "Setiawan", "Lestari", "Hidayat", "Nugraha" };

        var rand = new Random(100);
        for (int i = 11; i <= 45; i++)
        {
            var fn = firstNames[rand.Next(firstNames.Length)];
            var ln = lastNames[rand.Next(lastNames.Length)];
            var name = $"{fn} {ln}";
            var dept = depts[rand.Next(depts.Length)];
            var role = roles[rand.Next(roles.Length)];
            var status = rand.Next(10) > 2 ? "active" : "inactive";

            _users.Add(new User
            {
                Id = id++,
                FullName = name,
                Email = $"{fn.ToLower()}.{ln.ToLower()}{i}@ptpersada.co.id",
                Department = dept,
                Role = role,
                Status = status,
                TotalBookings = rand.Next(0, 30),
                Initials = GetInitials(name),
                EmployeeId = $"EMP-{1000 + i}",
                Phone = $"+62 812-{rand.Next(1000, 9999)}-{rand.Next(1000, 9999)}"
            });
        }
    }

    private static string GetInitials(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return "U";
        var parts = name.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 1) return parts[0].Substring(0, Math.Min(2, parts[0].Length)).ToUpperInvariant();
        return $"{parts[0][0]}{parts[^1][0]}".ToUpperInvariant();
    }

    public async Task<(IEnumerable<User> Users, int TotalCount)> GetUsersAsync(int page, int pageSize, string searchQuery, string roleFilter)
    {
        return await GetUsersFilteredAsync(page, pageSize, searchQuery, "All", roleFilter);
    }

    public async Task<(IEnumerable<User> Users, int TotalCount)> GetUsersFilteredAsync(int page, int pageSize, string searchQuery, string departmentFilter, string roleFilter = "All")
    {
        await Task.Delay(100); // quick network response

        var query = _users.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            var q = searchQuery.ToLowerInvariant();
            query = query.Where(u => u.FullName.ToLowerInvariant().Contains(q) ||
                                     u.Email.ToLowerInvariant().Contains(q) ||
                                     u.EmployeeId.ToLowerInvariant().Contains(q));
        }

        if (!string.IsNullOrWhiteSpace(departmentFilter) && departmentFilter != "All" && departmentFilter != "Semua Departemen")
        {
            query = query.Where(u => u.Department.Equals(departmentFilter, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(roleFilter) && roleFilter != "All" && roleFilter != "Semua Role")
        {
            query = query.Where(u => u.Role.Equals(roleFilter, StringComparison.OrdinalIgnoreCase));
        }

        var total = query.Count();
        var paged = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return (paged, total);
    }

    public Task<List<User>> GetAllUsersAsync()
    {
        return Task.FromResult(_users.ToList());
    }

    public Task<User?> GetUserByIdAsync(int id)
    {
        return Task.FromResult(_users.FirstOrDefault(u => u.Id == id));
    }

    public Task<bool> AddUserAsync(User user)
    {
        if (_users.Any(u => u.EmployeeId.Equals(user.EmployeeId, StringComparison.OrdinalIgnoreCase) || u.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase)))
        {
            return Task.FromResult(false);
        }

        user.Id = _users.Any() ? _users.Max(u => u.Id) + 1 : 1;
        user.Initials = GetInitials(user.FullName);
        _users.Insert(0, user);
        return Task.FromResult(true);
    }

    public Task<bool> UpdateUserAsync(User user)
    {
        var existing = _users.FirstOrDefault(u => u.Id == user.Id);
        if (existing == null) return Task.FromResult(false);

        existing.FullName = user.FullName;
        existing.Email = user.Email;
        existing.Department = user.Department;
        existing.EmployeeId = user.EmployeeId;
        existing.Phone = user.Phone;
        existing.Role = user.Role;
        existing.Status = user.Status;
        existing.Initials = GetInitials(user.FullName);
        return Task.FromResult(true);
    }

    public Task UpdateUserRoleAsync(int id, string newRole)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user != null) user.Role = newRole;
        return Task.CompletedTask;
    }

    public Task UpdateUserStatusAsync(int id, string newStatus)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user != null) user.Status = newStatus;
        return Task.CompletedTask;
    }

    public Task DeleteUserAsync(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user != null) _users.Remove(user);
        return Task.CompletedTask;
    }

    public Task<int> ImportUsersAsync(IEnumerable<User> users)
    {
        int count = 0;
        foreach (var u in users)
        {
            if (!_users.Any(existing => existing.Email.Equals(u.Email, StringComparison.OrdinalIgnoreCase)))
            {
                u.Id = _users.Any() ? _users.Max(x => x.Id) + 1 : 1;
                u.Initials = GetInitials(u.FullName);
                _users.Add(u);
                count++;
            }
        }
        return Task.FromResult(count);
    }
}
