namespace booking_room_admin.Components.Services;

/// <summary>
/// Helper untuk pemetaan ikon representatif dan styling warna berdasarkan jenis permasalahan/tiket.
/// </summary>
public static class IssueIconHelper
{
    public record IssueVisual(string IconName, string BgColor, string TextColor, string BorderColor);

    public static IssueVisual GetVisual(string titleOrCategory)
    {
        if (string.IsNullOrWhiteSpace(titleOrCategory))
            return new IssueVisual("alert-triangle", "#F3F4F6", "#4B5563", "#D1D5DB");

        var text = titleOrCategory.ToLowerInvariant();

        // 1. Email / Password
        if (text.Contains("email") || text.Contains("mail") || text.Contains("kata sandi") || text.Contains("password"))
            return new IssueVisual("mail", "#DBEAFE", "#1D4ED8", "#93C5FD");

        // 2. Server / Database / Jaringan / Infrastruktur
        if (text.Contains("server") || text.Contains("down") || text.Contains("database") || text.Contains("jaringan") || text.Contains("koneksi") || text.Contains("network"))
            return new IssueVisual("server", "#FEE2E2", "#DC2626", "#FCA5A5");

        // 3. Kalender / Penjadwalan / Sinkronisasi
        if (text.Contains("kalender") || text.Contains("calendar") || text.Contains("sinkron") || text.Contains("sync") || text.Contains("jadwal"))
            return new IssueVisual("calendar", "#FEF3C7", "#D97706", "#FCD34D");

        // 4. Form / 500 / Error Submit / Validasi / Bug Form
        if (text.Contains("form") || text.Contains("500") || text.Contains("submit") || text.Contains("error") || text.Contains("laporan") || text.Contains("bug") || text.Contains("crash"))
            return new IssueVisual("file-text", "#FEE2E2", "#B91C1C", "#F87171");

        // 5. Katering / Makanan & Minuman
        if (text.Contains("katering") || text.Contains("catering") || text.Contains("makanan") || text.Contains("snack") || text.Contains("coffee") || text.Contains("minum"))
            return new IssueVisual("coffee", "#FCE7F3", "#BE185D", "#F9A8D4");

        // 6. Fasilitas Umum / Permintaan Fasilitas
        if (text.Contains("fasilitas") || text.Contains("facility"))
            return new IssueVisual("package", "#DBEAFE", "#1D4ED8", "#93C5FD");

        // 7. AV / Proyektor / Layar / Sound
        if (text.Contains("av") || text.Contains("audio") || text.Contains("proyektor") || text.Contains("projector") || text.Contains("mic") || text.Contains("sound") || text.Contains("layar") || text.Contains("screen") || text.Contains("streaming") || text.Contains("video"))
            return new IssueVisual("monitor", "#EDE9FE", "#6D28D9", "#C4B5FD");

        // 8. Akun / Resign / Mutasi / User Deletion
        if (text.Contains("akun") || text.Contains("account") || text.Contains("resign") || text.Contains("mutasi") || text.Contains("hapus"))
            return new IssueVisual("user-x", "#FFEDD5", "#C2410C", "#FDBA74");

        // 9. Hardware / IT Umum
        if (text.Contains("laptop") || text.Contains("pc") || text.Contains("komputer") || text.Contains("printer") || text.Contains("hardware") || text.Contains("it"))
            return new IssueVisual("wrench", "#DBEAFE", "#2563EB", "#BFDBFE");

        return new IssueVisual("package", "#F3F4F6", "#4B5563", "#E5E7EB");
    }
}
