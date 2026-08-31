namespace booking_room_admin.Components.Helpers
{
    public static class IconHelper
    {
        public static string GetFacilityIcon(string facilityName)
        {
            var name = facilityName?.ToLowerInvariant() ?? "";
            
            if (name.Contains("wifi") || name.Contains("wi-fi") || name.Contains("internet")) return "wifi";
            if (name.Contains("tv") || name.Contains("televisi") || name.Contains("screen") || name.Contains("layar")) return "monitor";
            if (name.Contains("ac") || name.Contains("air") || name.Contains("pendingin")) return "wind";
            if (name.Contains("kopi") || name.Contains("minum") || name.Contains("coffee") || name.Contains("katering") || name.Contains("catering")) return "coffee";
            if (name.Contains("video") || name.Contains("kamera") || name.Contains("cam")) return "video";
            if (name.Contains("board") || name.Contains("papan")) return "edit-3";
            if (name.Contains("mic") || name.Contains("suara") || name.Contains("sound")) return "mic";
            if (name.Contains("proyektor") || name.Contains("projector")) return "projector";
            if (name.Contains("meja") || name.Contains("kursi") || name.Contains("furnitur")) return "armchair";
            if (name.Contains("kabel") || name.Contains("cable")) return "cable";
            if (name.Contains("bersih") || name.Contains("cleaning") || name.Contains("sapu")) return "sparkles";
            if (name.Contains("it") || name.Contains("komputer")) return "cpu";
            if (name.Contains("umum") || name.Contains("fasilitas")) return "package"; // Generic facility category
            
            return "tag"; // Fallback for custom facility
        }

        public static string GetNotificationIcon(string type)
        {
            var t = type?.ToLowerInvariant() ?? "";
            
            if (t.Contains("booking") || t.Contains("pesanan")) return "calendar-check";
            if (t.Contains("fasilitas") || t.Contains("permintaan")) return "package";
            if (t.Contains("sistem") || t.Contains("system")) return "server";
            if (t.Contains("batal") || t.Contains("tolak") || t.Contains("cancel")) return "x-circle";
            if (t.Contains("setuju") || t.Contains("approve") || t.Contains("sukses")) return "check-circle";
            
            return "bell"; // Fallback
        }
    }
}
