namespace booking_room_admin.Components.Services;

using booking_room_admin.Components.Models;

public interface IUserService
{
    Task<(IEnumerable<User> Users, int TotalCount)> GetUsersAsync(int page, int pageSize, string searchQuery, string roleFilter);
    Task<User?> GetUserByIdAsync(int id);
    Task UpdateUserRoleAsync(int id, string newRole);
    Task UpdateUserStatusAsync(int id, string newStatus);
    Task DeleteUserAsync(int id);
}

public class UserService : IUserService
{
    private readonly List<User> _users = new();

    public UserService()
    {
        // Generate 800 mock users to simulate large dataset
        var depts = new[] { "IT & Digital", "Human Resources", "Finance", "Marketing", "Operations", "Legal", "General Affairs" };
        var roles = new[] { "Admin", "Approver", "User", "User", "User", "User", "User", "User" };
        var statuses = new[] { "active", "active", "active", "active", "inactive" };
        
        var random = new Random(42); // fixed seed for consistency

        for (int i = 1; i <= 800; i++)
        {
            var dept = depts[random.Next(depts.Length)];
            var role = roles[random.Next(roles.Length)];
            var status = statuses[random.Next(statuses.Length)];
            var name = $"User Karyawan {i}";
            var initials = $"UK";
            
            _users.Add(new User
            {
                Id = i,
                FullName = name,
                Email = $"user{i}@persada.co.id",
                Department = dept,
                Role = role,
                Status = status,
                TotalBookings = random.Next(0, 50),
                Initials = initials,
                EmployeeId = $"EMP-{1000 + i}",
                Phone = $"+62 812-3456-{random.Next(1000, 9999)}"
            });
        }
    }

    public async Task<(IEnumerable<User> Users, int TotalCount)> GetUsersAsync(int page, int pageSize, string searchQuery, string roleFilter)
    {
        await Task.Delay(300); // Simulate network latency

        var query = _users.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            searchQuery = searchQuery.ToLowerInvariant();
            query = query.Where(u => u.FullName.ToLowerInvariant().Contains(searchQuery) || u.Email.ToLowerInvariant().Contains(searchQuery));
        }

        if (!string.IsNullOrWhiteSpace(roleFilter) && roleFilter != "All")
        {
            query = query.Where(u => u.Role.Equals(roleFilter, StringComparison.OrdinalIgnoreCase));
        }

        var total = query.Count();
        var paged = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return (paged, total);
    }

    public Task<User?> GetUserByIdAsync(int id)
    {
        return Task.FromResult(_users.FirstOrDefault(u => u.Id == id));
    }

    public Task UpdateUserRoleAsync(int id, string newRole)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            user.Role = newRole;
        }
        return Task.CompletedTask;
    }

    public Task UpdateUserStatusAsync(int id, string newStatus)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            user.Status = newStatus;
        }
        return Task.CompletedTask;
    }

    public Task DeleteUserAsync(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            _users.Remove(user);
        }
        return Task.CompletedTask;
    }
}
