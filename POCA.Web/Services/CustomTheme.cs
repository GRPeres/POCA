using MudBlazor;

public static class CustomTheme
{
    public static MudTheme MyTheme = new MudTheme()
    {
        // ===== LIGHT MODE =====
        PaletteLight = new PaletteLight()
        {
            Primary = "#011638",          // Oxford Blue (primary buttons/app bar)
            Secondary = "#3a7d44",       // Fern Green (secondary actions)
            Tertiary = "#04f06a",        // Spring Green (accent highlights)
            Background = "#fffcf7",       // Baby Powder (main background)
            AppbarBackground = "#011638", // Oxford Blue (app bar)
            DrawerBackground = "#f7f7f7", // Seasalt (drawer/sidebar)
            Surface = "#fffcf7",         // Baby Powder (cards/containers)
            TextPrimary = "#011638",     // Oxford Blue (primary text)
            TextSecondary = "#3a7d44",   // Fern Green (secondary text)
            Success = "#3a7d44",         // Reuse Fern Green for success
            Info = "#011638",            // Oxford Blue for info
            Warning = "#ff9800",         // Default orange
            Error = "#f44336",           // Default red
        },

        // ===== DARK MODE =====
        PaletteDark = new PaletteDark()
        {
            Primary = "#3a7d44",         // Fern Green becomes primary in dark mode
            Secondary = "#04f06a",       // Spring Green for secondary
            Tertiary = "#011638",        // Oxford Blue as accent
            Background = "#121212",      // Dark gray (Material Design standard)
            AppbarBackground = "#1a1a1a", // Slightly lighter than background
            DrawerBackground = "#1e1e1e", // Darker than appbar
            Surface = "#1e1e1e",         // Cards/containers
            TextPrimary = "#f7f7f7",     // Seasalt for primary text
            TextSecondary = "#04f06a",   // Spring Green for secondary text
            Success = "#3a7d44",         // Fern Green for success
            Info = "#04f06a",            // Spring Green for info
            Warning = "#ff9800",         // Default orange
            Error = "#f44336",           // Default red
        },

        // ===== TYPOGRAPHY & LAYOUT (Shared) =====
        Typography = new Typography()
        {
            Default = new DefaultTypography()
            {
                FontFamily = new[] { "Roboto", "Arial", "sans-serif" },
                FontSize = "0.875rem",
                FontWeight = "400",
                LineHeight = "1.5",
                LetterSpacing = "0.00938em"
            },
            H1 = new H1Typography()
            {
                FontSize = "2.5rem",
                FontWeight = "500",
                LineHeight = "1.2",
                LetterSpacing = "-0.00833em"
            }
        },

        LayoutProperties = new LayoutProperties()
        {
            DefaultBorderRadius = "4px", // Consistent rounded corners
        },
    };
}