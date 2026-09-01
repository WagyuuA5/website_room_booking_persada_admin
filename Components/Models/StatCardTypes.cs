namespace booking_room_admin.Components.Models;

/// <summary>
/// Definisi tipe unik untuk setiap Card Ringkasan / KPI.
/// Setiap tipe terikat pada 1 ikon Lucide unik dan 1 token warna semantik.
/// </summary>
public enum StatCardType
{
    TotalBooking,
    PopularRoom,
    TotalFacilityRequest,
    TotalAccountDeletionRequest,
    TotalTicket,
    OpenTicket,
    TotalBugReport,
    CriticalSeverity,
    TotalSystemRequest,
    Unassigned,
    ApprovalRate,
    AverageDuration,
    CompletedRequest,
    SpecialCatering,
    AverageResponseTime,
    ApprovedDeletion,
    PendingVerification,
    RejectedDeletion,
    ResolvedTicket,
    AverageHandlingTime,
    ResolvedBug,
    AverageResolutionTime,
    CompletionRate,
    TodayActivity,
    TotalAuditLog,
    ConfigChanges,
    ActiveUsers
}

/// <summary>
/// Kamus pemetaan terpusat StatCardType -> (Nama Ikon Lucide, Varian Warna Token).
/// Mencegah duplikasi visual ikon antar jenis KPI yang berbeda.
/// </summary>
public static class StatCardIconMap
{
    public static (string Icon, string Variant) Get(StatCardType type) => type switch
    {
        StatCardType.TotalBooking => ("calendar-check", "accent"),
        StatCardType.PopularRoom => ("building-2", "warning"),
        StatCardType.TotalFacilityRequest => ("package-search", "accent"),
        StatCardType.TotalAccountDeletionRequest => ("user-minus", "danger"),
        StatCardType.TotalTicket => ("ticket", "accent"),
        StatCardType.OpenTicket => ("folder-open", "warning"),
        StatCardType.TotalBugReport => ("bug", "accent"),
        StatCardType.CriticalSeverity => ("triangle-alert", "danger"),
        StatCardType.TotalSystemRequest => ("layers", "accent"),
        StatCardType.Unassigned => ("clipboard-x", "warning"),
        StatCardType.ApprovalRate => ("check-circle", "success"),
        StatCardType.AverageDuration => ("clock", "info"),
        StatCardType.CompletedRequest => ("check-circle", "success"),
        StatCardType.SpecialCatering => ("coffee", "warning"),
        StatCardType.AverageResponseTime => ("clock", "info"),
        StatCardType.ApprovedDeletion => ("check-circle", "success"),
        StatCardType.PendingVerification => ("clock", "warning"),
        StatCardType.RejectedDeletion => ("x-circle", "danger"),
        StatCardType.ResolvedTicket => ("check-circle", "success"),
        StatCardType.AverageHandlingTime => ("clock", "info"),
        StatCardType.ResolvedBug => ("check-circle", "success"),
        StatCardType.AverageResolutionTime => ("clock", "info"),
        StatCardType.CompletionRate => ("check-circle", "success"),
        StatCardType.TodayActivity => ("activity", "info"),
        StatCardType.TotalAuditLog => ("file-text", "accent"),
        StatCardType.ConfigChanges => ("settings", "warning"),
        StatCardType.ActiveUsers => ("users", "info"),
        _ => ("bar-chart-2", "accent")
    };
}
