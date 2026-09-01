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
            new Room { 
                Id = 1, 
                Name = "Ruang Dirantara", 
                Description = "Ruang rapat utama dengan display 4K dan sistem video konferensi terintegrasi.", 
                Capacity = 20, 
                Floor = "Lantai 3", 
                Type = "Rapat", 
                Status = "active", 
                TotalBookings = 124, 
                Facilities = new List<string>{ "WIFI", "PROYEKTOR", "VIDEO CALL", "AC", "TV" },
                PhotoUrl = "https://images.unsplash.com/photo-1517502884422-41eaead166d4?w=800&auto=format&fit=crop&q=80",
                GalleryPhotos = new List<string>{ "https://images.unsplash.com/photo-1517502884422-41eaead166d4?w=800&auto=format&fit=crop&q=80", "https://images.unsplash.com/photo-1497366216548-37526070297c?w=800&auto=format&fit=crop&q=80" },
                PinX = 35.0,
                PinY = 40.0,
                LocationNotes = "Lantai 3, Sayap Utama — dekat lift eksekutif"
            },
            new Room { 
                Id = 2, 
                Name = "Ruang Rapat A", 
                Description = "Ruang diskusi tim dengan papan tulis kaca dan layar display presisi.", 
                Capacity = 10, 
                Floor = "Lantai 2", 
                Type = "Diskusi", 
                Status = "active", 
                TotalBookings = 89, 
                Facilities = new List<string>{ "WIFI", "PAPAN TULIS", "TV", "AC" },
                PhotoUrl = "https://images.unsplash.com/photo-1497366216548-37526070297c?w=800&auto=format&fit=crop&q=80",
                GalleryPhotos = new List<string>{ "https://images.unsplash.com/photo-1497366216548-37526070297c?w=800&auto=format&fit=crop&q=80" },
                PinX = 65.0,
                PinY = 30.0,
                LocationNotes = "Lantai 2, Sayap Barat — samping area pantry"
            },
            new Room { 
                Id = 3, 
                Name = "Ruang Boardroom", 
                Description = "Ruang eksekutif privat untuk pertemuan direksi dan pemangku kepentingan.", 
                Capacity = 12, 
                Floor = "Lantai 14", 
                Type = "Eksekutif", 
                Status = "maintenance", 
                TotalBookings = 45, 
                Facilities = new List<string>{ "WIFI", "VIDEO CALL", "AC", "SOUND SYSTEM" }, 
                MaintenanceEnd = DateTime.Now.AddHours(2),
                PhotoUrl = "https://images.unsplash.com/photo-1497366811353-6870744d04b2?w=800&auto=format&fit=crop&q=80",
                GalleryPhotos = new List<string>{ "https://images.unsplash.com/photo-1497366811353-6870744d04b2?w=800&auto=format&fit=crop&q=80" },
                PinX = 50.0,
                PinY = 55.0,
                LocationNotes = "Lantai 14, Area Direksi — akses kartu khusus"
            },
            new Room { 
                Id = 4, 
                Name = "Ruang Training", 
                Description = "Ruang pelatihan fleksibel dengan tata letak modular dan tata suara profesional.", 
                Capacity = 40, 
                Floor = "Lantai 1", 
                Type = "Presentasi", 
                Status = "active", 
                TotalBookings = 67, 
                Facilities = new List<string>{ "WIFI", "PROYEKTOR", "SOUND SYSTEM", "AC", "KURSI" },
                PhotoUrl = "https://images.unsplash.com/photo-1524178232363-1fb2b075b655?w=800&auto=format&fit=crop&q=80",
                GalleryPhotos = new List<string>{ "https://images.unsplash.com/photo-1524178232363-1fb2b075b655?w=800&auto=format&fit=crop&q=80" },
                PinX = 25.0,
                PinY = 70.0,
                LocationNotes = "Lantai 1, Hall Utama — depan lobby barat"
            },
            new Room { 
                Id = 5, 
                Name = "Ruang Diskusi C", 
                Description = "Ruang brainstorming dinamis dengan tempat duduk santai dan pencahayaan alami.", 
                Capacity = 6, 
                Floor = "Lantai 2", 
                Type = "Diskusi", 
                Status = "active", 
                TotalBookings = 34, 
                Facilities = new List<string>{ "WIFI", "PAPAN TULIS", "MEJA", "AC" },
                PhotoUrl = "https://images.unsplash.com/photo-1577495508048-b635879837f1?w=800&auto=format&fit=crop&q=80",
                GalleryPhotos = new List<string>{ "https://images.unsplash.com/photo-1577495508048-b635879837f1?w=800&auto=format&fit=crop&q=80" },
                PinX = 75.0,
                PinY = 60.0,
                LocationNotes = "Lantai 2, Sayap Timur — dekat tangga darurat"
            },
            new Room { 
                Id = 6, 
                Name = "Ruang Rapat Direksi", 
                Description = "Ruang rapat direksi dengan meja konferensi jati dan fasilitas dokumentasi digital.", 
                Capacity = 16, 
                Floor = "Lantai 14", 
                Type = "Eksekutif", 
                Status = "active", 
                TotalBookings = 28, 
                Facilities = new List<string>{ "WIFI", "VIDEO CALL", "PROYEKTOR", "AC", "TV" },
                PhotoUrl = "https://images.unsplash.com/photo-1431540015161-0bf868a2d407?w=800&auto=format&fit=crop&q=80",
                GalleryPhotos = new List<string>{ "https://images.unsplash.com/photo-1431540015161-0bf868a2d407?w=800&auto=format&fit=crop&q=80" },
                PinX = 60.0,
                PinY = 45.0,
                LocationNotes = "Lantai 14, Sayap VIP — koridor barat"
            }
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
        if (string.IsNullOrEmpty(room.PhotoUrl))
        {
            room.PhotoUrl = "https://images.unsplash.com/photo-1497366216548-37526070297c?w=800&auto=format&fit=crop&q=80";
        }
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
            existing.PhotoUrl = room.PhotoUrl;
            existing.GalleryPhotos = room.GalleryPhotos;
            existing.PinX = room.PinX;
            existing.PinY = room.PinY;
            existing.LocationNotes = room.LocationNotes;
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
