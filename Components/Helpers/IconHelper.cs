namespace booking_room_admin.Components.Helpers
{
    public static class IconHelper
    {
        public static string GetFacilityIcon(string facilityName)
        {
            var name = facilityName?.ToLowerInvariant() ?? "";
            
            if (name.Contains("wifi") || name.Contains("wi-fi") || name.Contains("internet")) return "wifi";
            if (name.Contains("tv") || name.Contains("televisi") || name.Contains("screen") || name.Contains("layar")) return "tv";
            if (name.Contains("ac") || name.Contains("air") || name.Contains("pendingin")) return "wind";
            if (name.Contains("video") || name.Contains("konferensi") || name.Contains("teleconference")) return "video";
            if (name.Contains("proyektor") || name.Contains("projector")) return "projector";
            if (name.Contains("sound") || name.Contains("audio") || name.Contains("speaker") || name.Contains("suara")) return "speaker";
            if (name.Contains("papan") || name.Contains("board") || name.Contains("whiteboard") || name.Contains("smart board") || name.Contains("tulis")) return "presentation";
            if (name.Contains("meja") || name.Contains("desk") || name.Contains("table")) return "table-2";
            if (name.Contains("kursi") || name.Contains("chair") || name.Contains("sofa") || name.Contains("armchair")) return "armchair";
            if (name.Contains("kabel") || name.Contains("cable") || name.Contains("colokan") || name.Contains("stopkontak")) return "cable";
            if (name.Contains("webcam") || name.Contains("kamera") || name.Contains("cam")) return "webcam";
            if (name.Contains("soundproof") || name.Contains("kedap")) return "volume-x";
            if (name.Contains("kopi") || name.Contains("minum") || name.Contains("coffee") || name.Contains("katering") || name.Contains("catering")) return "coffee";
            if (name.Contains("bersih") || name.Contains("cleaning") || name.Contains("sapu")) return "sparkles";
            if (name.Contains("mic") || name.Contains("mikrofon")) return "mic";
            if (name.Contains("it") || name.Contains("komputer")) return "cpu";
            if (name.Contains("umum") || name.Contains("fasilitas")) return "package";
            
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
