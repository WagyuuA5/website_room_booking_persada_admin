using booking_room_admin.Components.Models;

namespace booking_room_admin.Components.Services;

public interface IRoomService
{
    Task<List<Room>> GetAllRoomsAsync();
    Task<Room?> GetRoomByIdAsync(int id);
    Task<Room> AddRoomAsync(Room room);
    Task UpdateRoomAsync(Room room);
    Task DeleteRoomAsync(int id);
}

public class RoomService : IRoomService
{
    private List<Room> _rooms;

    public RoomService()
    {
        _rooms = new List<Room>
        {
            new Room { Id = 1, Name = "Ruang Dirantara", Description = "Ruang rapat utama dengan projector 4K dan sistem video konferensi.", Capacity = 20, Floor = "Lantai 3", Type = "Rapat", Status = "active", TotalBookings = 124, Facilities = new List<string>{ "WiFi", "Proyektor", "Video Konferensi", "AC" } },
            new Room { Id = 2, Name = "Ruang Rapat A", Description = "Ruang diskusi tim dengan whiteboard dan display HD.", Capacity = 10, Floor = "Lantai 2", Type = "Diskusi", Status = "active", TotalBookings = 89, Facilities = new List<string>{ "WiFi", "Whiteboard", "Display" } },
            new Room { Id = 3, Name = "Ruang Boardroom", Description = "Ruang eksekutif privat untuk pertemuan dewan dan klien penting.", Capacity = 12, Floor = "Lantai 14", Type = "Eksekutif", Status = "maintenance", TotalBookings = 45, Facilities = new List<string>{ "WiFi", "Video Konferensi", "Katering", "AC" }, MaintenanceEnd = DateTime.Now.AddHours(2) },
            new Room { Id = 4, Name = "Ruang Training", Description = "Ruang pelatihan dengan setup kursi theater dan sound system.", Capacity = 40, Floor = "Lantai 1", Type = "Presentasi", Status = "active", TotalBookings = 67, Facilities = new List<string>{ "WiFi", "Proyektor", "Sound System", "AC" } },
            new Room { Id = 5, Name = "Ruang Diskusi C", Description = "Ruang brainstorming kecil dengan sofa dan lighting fleksibel.", Capacity = 6, Floor = "Lantai 2", Type = "Diskusi", Status = "active", TotalBookings = 34, Facilities = new List<string>{ "WiFi", "Whiteboard" } },
            new Room { Id = 6, Name = "Ruang Rapat Direksi", Description = "Ruang rapat direksi dengan meja konferensi premium dan sistem dokumentasi.", Capacity = 16, Floor = "Lantai 14", Type = "Eksekutif", Status = "active", TotalBookings = 28, Facilities = new List<string>{ "WiFi", "Video Konferensi", "Katering", "Proyektor", "AC" } }
        };
    }

    public Task<List<Room>> GetAllRoomsAsync()
    {
        return Task.FromResult(_rooms.ToList());
    }

    public Task<Room?> GetRoomByIdAsync(int id)
    {
        return Task.FromResult(_rooms.FirstOrDefault(r => r.Id == id));
    }

    public Task<Room> AddRoomAsync(Room room)
    {
        room.Id = _rooms.Any() ? _rooms.Max(r => r.Id) + 1 : 1;
        _rooms.Add(room);
        return Task.FromResult(room);
    }

    public Task UpdateRoomAsync(Room room)
    {
        var existing = _rooms.FirstOrDefault(r => r.Id == room.Id);
        if (existing != null)
        {
            existing.Name = room.Name;
            existing.Description = room.Description;
            existing.Capacity = room.Capacity;
            existing.Floor = room.Floor;
            existing.Type = room.Type;
            existing.Status = room.Status;
            existing.TotalBookings = room.TotalBookings;
            existing.Facilities = room.Facilities;
            existing.MaintenanceEnd = room.MaintenanceEnd;
        }
        return Task.CompletedTask;
    }

    public Task DeleteRoomAsync(int id)
    {
        var existing = _rooms.FirstOrDefault(r => r.Id == id);
        if (existing != null)
        {
            _rooms.Remove(existing);
        }
        return Task.CompletedTask;
    }
}
