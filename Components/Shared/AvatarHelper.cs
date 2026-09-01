namespace booking_room_admin.Components.Shared
{
    public static class AvatarHelper
    {
        public static readonly string[] AvatarPalette = new[]
        {
            "#2563EB",
            "#0D9488",
            "#7C3AED",
            "#B45309",
            "#BE185D",
            "#475569"
        };

        public static string GetColor(string? name)
        {
            if (string.IsNullOrWhiteSpace(name)) return AvatarPalette[5];
            int hash = 0;
            foreach (char c in name.Trim())
            {
                hash = (hash * 31) + c;
            }
            return AvatarPalette[System.Math.Abs(hash) % AvatarPalette.Length];
        }

        public static string GetInitials(string? name)
        {
            if (string.IsNullOrWhiteSpace(name)) return "U";
            var parts = name.Trim().Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2)
                return (parts[0].Substring(0, 1) + parts[parts.Length - 1].Substring(0, 1)).ToUpperInvariant();
            return parts[0].Substring(0, System.Math.Min(2, parts[0].Length)).ToUpperInvariant();
        }
    }
}
