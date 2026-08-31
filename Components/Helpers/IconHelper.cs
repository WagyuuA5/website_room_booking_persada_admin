namespace booking_room_admin.Components.Helpers
{
    public static class IconHelper
    {
        public static string GetFacilityIcon(string facilityName)
        {
            var nama = facilityName?.ToLowerInvariant().Trim() ?? "";
            
            // 1. WIFI / Internet / Jaringan
            if (nama.Contains("wifi") || nama.Contains("wi-fi") || nama.Contains("internet") || nama.Contains("hotspot") || nama.Contains("bandwidth")) return "wifi";

            // 2. VIDEO CALL / Telekonferensi / Zoom / Kamera
            if (nama.Contains("video call") || nama.Contains("video") || nama.Contains("zoom") || nama.Contains("telekonferensi") || nama.Contains("konferensi video") || nama.Contains("vicon") || nama.Contains("webcam") || nama.Contains("kamera")) return "video";

            // 3. PROYEKTOR / DUAL PROYEKTOR / Projector
            if (nama.Contains("proyektor") || nama.Contains("projector") || nama.Contains("infocus")) return "projector";

            // 4. TV / Display / Monitor
            if (nama.Contains("tv") || nama.Contains("televisi") || nama.Contains("display") || nama.Contains("layar tv")) return "tv";

            // 5. AC / Pendingin / Pengatur Suhu / HVAC / Wind
            if (nama.Contains("ac") || nama.Contains("pendingin") || nama.Contains("suhu") || nama.Contains("angin") || nama.Contains("wind") || nama.Contains("hvac")) return "wind";

            // 6. SOUND SYSTEM / Audio / Speaker
            if (nama.Contains("sound system") || nama.Contains("sound") || nama.Contains("speaker") || nama.Contains("audio") || nama.Contains("pengeras suara") || nama.Contains("tata suara")) return "speaker";
            if (nama.Contains("mikrofon") || nama.Contains("microphone") || nama.Contains("mic") || nama.Contains("clip-on") || nama.Contains("podium")) return "mic";

            // 7. PAPAN TULIS / SMART BOARD / Whiteboard / Presentation
            if (nama.Contains("smart board") || nama.Contains("smartboard") || nama.Contains("papan tulis") || nama.Contains("whiteboard") || nama.Contains("spidol") || nama.Contains("flipchart") || nama.Contains("presentasi")) return "presentation";

            // 8. MEJA / Table
            if (nama.Contains("meja") || nama.Contains("table") || nama.Contains("desk")) return "table-2";

            // 9. KURSI / Armchair / Tempat Duduk
            if (nama.Contains("kursi") || nama.Contains("chair") || nama.Contains("armchair") || nama.Contains("sofa") || nama.Contains("duduk") || nama.Contains("ergonomis")) return "armchair";

            // 10. Kabel / Stopkontak / Listrik / Plug
            if (nama.Contains("kabel") || nama.Contains("hdmi") || nama.Contains("vga") || nama.Contains("adaptor") || nama.Contains("konektor") || nama.Contains("extension")) return "cable";
            if (nama.Contains("stopkontak") || nama.Contains("colokan") || nama.Contains("listrik") || nama.Contains("power") || nama.Contains("plug") || nama.Contains("terminal")) return "plug";

            // 11. Lampu / Pencahayaan
            if (nama.Contains("lampu") || nama.Contains("cahaya") || nama.Contains("pencahayaan") || nama.Contains("lighting")) return "lightbulb";

            // 12. Konsumsi / Katering / Makanan / Minuman (HANYA untuk konsumsi spesifik)
            if (nama.Contains("konsumsi") || nama.Contains("katering") || nama.Contains("catering") || nama.Contains("kopi") || nama.Contains("teh") || nama.Contains("snack") || nama.Contains("makanan") || nama.Contains("minuman")) return "coffee";

            // 13. Toilet / Kamar Mandi
            if (nama.Contains("toilet") || nama.Contains("wc") || nama.Contains("kamar mandi")) return "bath";

            // 14. Fallback Default untuk Fasilitas Custom / Tidak Dikenali -> "package" (Bukan tag, bukan alert-circle)
            return "package";
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
