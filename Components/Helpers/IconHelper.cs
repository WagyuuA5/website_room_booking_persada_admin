using System.Text.RegularExpressions;

namespace booking_room_admin.Components.Helpers
{
    public static class IconHelper
    {
        public static string GetFacilityIcon(string facilityName)
        {
            var nama = facilityName?.ToLowerInvariant().Trim() ?? "";
            
            // 1. Skenario Ruangan Kapasitas Besar
            if (nama.Contains("ac sentral") || nama.Contains("ac besar") || nama.Contains("kapasitas besar") || nama.Contains("central ac")) return "wind";
            if (nama.Contains("sound system") || nama.Contains("mikrofon wireless") || nama.Contains("mikrofon podium") || nama.Contains("clip-on") || nama.Contains("wireless mic")) return "mic-2";
            if (nama.Contains("multi-screen") || nama.Contains("wall screen") || nama.Contains("led") || nama.Contains("layar ganda") || nama.Contains("dual proyektor")) return "monitor-play";
            if (nama.Contains("dedicated bandwidth") || nama.Contains("bandwidth khusus") || nama.Contains("wifi dedicated")) return "gauge";
            if (nama.Contains("roll kabel") || nama.Contains("power strip") || nama.Contains("extension") || nama.Contains("terminal listrik")) return "cable";

            // 2. Fasilitas Utama (Presentasi & Konektivitas)
            if (nama.Contains("proyektor") || nama.Contains("projector")) return "projector";
            if (nama.Contains("layar tv") || nama.Contains("tv") || nama.Contains("televisi")) return "tv";
            if (nama.Contains("kabel") || nama.Contains("hdmi") || nama.Contains("vga") || nama.Contains("adaptor") || nama.Contains("konektor")) return "cable";
            if (nama.Contains("papan tulis") || nama.Contains("whiteboard") || nama.Contains("spidol")) return "presentation";
            if (nama.Contains("wifi") || nama.Contains("wi-fi") || nama.Contains("internet") || nama.Contains("koneksi internet")) return "wifi";
            if (nama.Contains("mikrofon") || nama.Contains("microphone") || nama.Contains("mic")) return "mic";
            if (nama.Contains("audio") || nama.Contains("pengeras suara") || nama.Contains("speaker") || nama.Contains("sound")) return "volume-2";

            // 3. Kenyamanan Ruangan
            if (nama.Contains("ac") || nama.Contains("pendingin") || nama.Contains("suhu") || nama.Contains("pengatur suhu")) return "wind";
            if (nama.Contains("lampu") || nama.Contains("cahaya") || nama.Contains("pencahayaan") || nama.Contains("lighting")) return "lightbulb";
            if (nama.Contains("meja") || nama.Contains("table")) return "table-2";
            if (nama.Contains("kursi") || nama.Contains("chair") || nama.Contains("ergonomis")) return "armchair";
            if (nama.Contains("stopkontak") || nama.Contains("colokan") || nama.Contains("listrik") || nama.Contains("power") || nama.Contains("plug")) return "plug";

            // 4. Fasilitas Tambahan (Opsional)
            if (nama.Contains("konsumsi") || nama.Contains("kopi") || nama.Contains("teh") || nama.Contains("snack") || nama.Contains("makanan") || nama.Contains("minuman")) return "coffee";
            if (nama.Contains("toilet") || nama.Contains("wc") || nama.Contains("kamar mandi")) return "bath";
            
            // 5. Fallback bertingkat - JANGAN langsung alert-circle
            return GetSmartFallbackIcon(nama);
        }

        private static string GetSmartFallbackIcon(string nama)
        {
            if (Regex.IsMatch(nama, "elektronik|listrik|daya|power")) return "plug";
            if (Regex.IsMatch(nama, "suara|bunyi|sound")) return "volume-2";
            if (Regex.IsMatch(nama, "layar|tampil|visual")) return "monitor";
            if (Regex.IsMatch(nama, "meja|kursi|duduk")) return "armchair";
            
            // Fallback akhir netral, BUKAN alert-circle
            return "tag"; 
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
