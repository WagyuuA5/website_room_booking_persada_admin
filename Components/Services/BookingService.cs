using System;
using System.Collections.Generic;
using System.Linq;
using booking_room_admin.Components.Models;

namespace booking_room_admin.Components.Services
{
    public interface IBookingService
    {
        List<Booking> GetAllBookings();
        void AddBooking(Booking booking);
        void UpdateBooking(Booking booking);
        bool CheckConflict(string room, DateTime date, string startTime, string endTime);
    }

    public class BookingService : IBookingService
    {
        private readonly List<Booking> _bookings;

        public BookingService()
        {
            _bookings = new List<Booking>
            {
                new Booking
                {
                    Id = 1,
                    Title = "Sesi Strategi Q3",
                    Room = "Ruang Dirantara",
                    Date = new DateTime(2026, 10, 15),
                    StartTime = "09:00",
                    EndTime = "11:00",
                    Duration = "2 Jam",
                    RequesterName = "Ahmad Fauzi",
                    RequesterDivision = "Operasional",
                    RequesterInitials = "AF",
                    ParticipantsCount = 8,
                    Status = "pending",
                    IsRecurring = false,
                    AccentColor = "warning"
                },
                new Booking
                {
                    Id = 2,
                    Title = "Review Produk Mingguan",
                    Room = "Ruang Rapat A",
                    Date = new DateTime(2026, 10, 15),
                    StartTime = "13:00",
                    EndTime = "14:30",
                    Duration = "1 Jam 30 Menit",
                    RequesterName = "Siti Nurhaliza",
                    RequesterDivision = "IT & Digital",
                    RequesterInitials = "SN",
                    ParticipantsCount = 12,
                    Status = "approved",
                    IsRecurring = true,
                    AccentColor = "info"
                },
                new Booking
                {
                    Id = 3,
                    Title = "Onboarding Karyawan Baru",
                    Room = "Ruang Diskusi",
                    Date = new DateTime(2026, 10, 16),
                    StartTime = "10:00",
                    EndTime = "12:00",
                    Duration = "2 Jam",
                    RequesterName = "Budi Santoso",
                    RequesterDivision = "Sumber Daya Manusia (SDM)",
                    RequesterInitials = "BS",
                    ParticipantsCount = 6,
                    Status = "pending",
                    IsRecurring = false,
                    AccentColor = "danger"
                },
                new Booking
                {
                    Id = 4,
                    Title = "Sprint Planning",
                    Room = "Ruang Rapat B",
                    Date = new DateTime(2026, 10, 16),
                    StartTime = "14:00",
                    EndTime = "16:00",
                    Duration = "2 Jam",
                    RequesterName = "Dewi Kartika",
                    RequesterDivision = "Teknikal & Pemeliharaan Telekomunikasi",
                    RequesterInitials = "DK",
                    ParticipantsCount = 10,
                    Status = "approved",
                    IsRecurring = true,
                    AccentColor = "info"
                },
                new Booking
                {
                    Id = 5,
                    Title = "Evaluasi Vendor",
                    Room = "Ruang Rapat Utama",
                    Date = new DateTime(2026, 10, 17),
                    StartTime = "09:30",
                    EndTime = "11:30",
                    Duration = "2 Jam",
                    RequesterName = "Rizky Hidayat",
                    RequesterDivision = "General Affairs (GA)",
                    RequesterInitials = "RH",
                    ParticipantsCount = 15,
                    Status = "rejected_system",
                    IsRecurring = false,
                    AccentColor = "warning"
                },
                new Booking
                {
                    Id = 6,
                    Title = "Workshop Digital Marketing",
                    Room = "Ruang Dirantara",
                    Date = new DateTime(2026, 10, 17),
                    StartTime = "13:00",
                    EndTime = "15:00",
                    Duration = "2 Jam",
                    RequesterName = "Maya Putri",
                    RequesterDivision = "Marketing & Komunikasi",
                    RequesterInitials = "MP",
                    ParticipantsCount = 20,
                    Status = "rejected_admin",
                    IsRecurring = false,
                    AccentColor = "danger"
                }
            };
        }

        public List<Booking> GetAllBookings() => _bookings.ToList();

        public void AddBooking(Booking booking)
        {
            booking.Id = _bookings.Any() ? _bookings.Max(b => b.Id) + 1 : 1;
            _bookings.Add(booking);
        }

        public void UpdateBooking(Booking booking)
        {
            var existing = _bookings.FirstOrDefault(b => b.Id == booking.Id);
            if (existing != null)
            {
                existing.Status = booking.Status;
                existing.Title = booking.Title;
                existing.Room = booking.Room;
                existing.Date = booking.Date;
                existing.StartTime = booking.StartTime;
                existing.EndTime = booking.EndTime;
                existing.Duration = booking.Duration;
                existing.RequesterName = booking.RequesterName;
                existing.RequesterDivision = booking.RequesterDivision;
                existing.RequesterInitials = booking.RequesterInitials;
                existing.ParticipantsCount = booking.ParticipantsCount;
                existing.IsRecurring = booking.IsRecurring;
                existing.AccentColor = booking.AccentColor;
            }
        }

        public bool CheckConflict(string room, DateTime date, string startTime, string endTime)
        {
            return _bookings.Any(b => b.Room == room && b.Date.Date == date.Date &&
                b.Status != "rejected_system" && b.Status != "rejected_admin" &&
                ((string.Compare(b.StartTime, endTime) < 0 && string.Compare(b.EndTime, startTime) > 0)));
        }
    }
}
