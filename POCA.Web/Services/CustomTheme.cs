using MudBlazor;

public static class CustomTheme
{
    public static MudTheme MyTheme = new MudTheme()
    {
        PaletteLight = new PaletteLight()
        {
            // ===== CORE COLORS =====
            Primary = "#3a7d44",         // Fern Green - main accent
            Secondary = "#011638",       // Oxford Blue - secondary accent
            Tertiary = "#04f06a",        // Spring Green - rare highlights

            // ===== BACKGROUNDS =====
            Background = "#fffcf7",      // Baby Powder - main background
            AppbarBackground = "#fffcf7",// Light appbar (was blue)
            DrawerBackground = "#f7f7f7",// Seasalt - subtle contrast
            Surface = "#fffcf7",         // Cards/paper same as background
            OverlayDark = "rgba(33,33,33,0.15)", // Subtle overlay

            // ===== TEXT =====
            TextPrimary = "#333333",     // Dark gray (better readability)
            TextSecondary = "#3a7d44",  // Fern Green
            TextDisabled = "#aaaaaa",

            // ===== STATES =====
            Success = "#3a7d44",        // Fern Green
            Info = "#011638",            // Oxford Blue
            Warning = "#ff9800",
            Error = "#f44336",

            // ===== COMPONENTS =====
            Divider = "#e0e0e0",         // Very light gray
            DividerLight = "#f5f5f5",    // Nearly white
            LinesDefault = "#e0e0e0",    // Borders
            LinesInputs = "#bdbdbd",     // Input borders
            TableLines = "#e0e0e0",
            TableStriped = "rgba(0,0,0,0.02)",
            TableHover = "rgba(0,0,0,0.04)"
        },

        PaletteDark = new PaletteDark()
        {
            // ===== CORE COLORS =====
            Primary = "#3a7d44",         // Fern Green stays primary
            Secondary = "#04f06a",       // Spring Green as secondary
            Tertiary = "#011638",        // Oxford Blue as tertiary

            // ===== BACKGROUNDS =====
            Background = "#121212",      // True dark mode
            AppbarBackground = "#1e1e1e",
            DrawerBackground = "#1a1a1a",
            Surface = "#1e1e1e",
            OverlayDark = "rgba(0,0,0,0.5)",

            // ===== TEXT =====
            TextPrimary = "#ffffff",     // White
            TextSecondary = "#04f06a",   // Spring Green
            TextDisabled = "#666666",

            // ===== STATES =====
            Success = "#3a7d44",
            Info = "#04f06a",
            Warning = "#ff9800",
            Error = "#f44336",

            // ===== COMPONENTS =====
            Divider = "#333333",
            DividerLight = "#252525",
            LinesDefault = "#333333",
            LinesInputs = "#555555",
            TableLines = "#333333",
            TableStriped = "rgba(255,255,255,0.02)",
            TableHover = "rgba(255,255,255,0.04)"
        },

        // ===== TYPOGRAPHY =====
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
            DefaultBorderRadius = "4px",
        }
    };
}