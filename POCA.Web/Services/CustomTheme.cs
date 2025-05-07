using MudBlazor;

public static class CustomTheme
{
    public static MudTheme MyTheme = new MudTheme()
    {
        PaletteLight = new PaletteLight()
        {
            // ===== CORE COLORS =====
            Primary = "#2E7D32",         // Material Green 800 (stronger contrast)
            Secondary = "#1B5E20",       // Material Green 900 (darker accent)
            Tertiary = "#4CAF50",        // Material Green 500 (highlight)

            // ===== BACKGROUNDS =====
            Background = "#FFFFFF",      // Pure white (Material standard)
            AppbarBackground = "#FFFFFF",// White appbar
            DrawerBackground = "#F5F5F5",// Material Grey 100
            Surface = "#FFFFFF",         // White surface for cards
            OverlayDark = "rgba(0,0,0,0.12)", // Material standard overlay

            // ===== TEXT =====
            TextPrimary = "rgba(0, 0, 0, 0.87)", // Material high-emphasis text
            TextSecondary = "rgba(0, 0, 0, 0.6)", // Medium emphasis
            TextDisabled = "rgba(0, 0, 0, 0.38)", // Disabled text

            // ===== STATES =====
            Success = "#2E7D32",         // Material Green 800
            Info = "#2196F3",            // Material Blue 500
            Warning = "#FF9800",         // Material Orange 500
            Error = "#F44336",          // Material Red 500

            // ===== COMPONENTS =====
            Divider = "rgba(0, 0, 0, 0.12)", // Material divider
            DividerLight = "rgba(0, 0, 0, 0.08)",
            LinesDefault = "rgba(0, 0, 0, 0.12)",
            LinesInputs = "rgba(0, 0, 0, 0.24)",
            TableLines = "rgba(0, 0, 0, 0.12)",
            TableStriped = "rgba(0, 0, 0, 0.02)",
            TableHover = "rgba(0, 0, 0, 0.04)"
        },

        PaletteDark = new PaletteDark()
        {
            // ===== CORE COLORS =====
            Primary = "#81C784",         // Material Green 300 (lighter for dark mode)
            Secondary = "#66BB6A",       // Material Green 400
            Tertiary = "#4CAF50",        // Material Green 500

            // ===== BACKGROUNDS =====
            Background = "#121212",      // Material Dark background
            AppbarBackground = "#1E1E1E",// Darker surface
            DrawerBackground = "#1E1E1E",
            Surface = "#1E1E1E",         // Dark surface for cards
            OverlayDark = "rgba(0,0,0,0.5)",

            // ===== TEXT =====
            TextPrimary = "rgba(255, 255, 255, 0.87)", // High emphasis
            TextSecondary = "rgba(255, 255, 255, 0.6)", // Medium
            TextDisabled = "rgba(255, 255, 255, 0.38)", // Disabled

            // ===== STATES =====
            Success = "#81C784",         // Material Green 300
            Info = "#64B5F6",            // Material Blue 300
            Warning = "#FFB74D",         // Material Orange 300
            Error = "#E57373",           // Material Red 300

            // ===== COMPONENTS =====
            Divider = "rgba(255, 255, 255, 0.12)",
            DividerLight = "rgba(255, 255, 255, 0.08)",
            LinesDefault = "rgba(255, 255, 255, 0.12)",
            LinesInputs = "rgba(255, 255, 255, 0.3)",
            TableLines = "rgba(255, 255, 255, 0.12)",
            TableStriped = "rgba(255, 255, 255, 0.02)",
            TableHover = "rgba(255, 255, 255, 0.04)"
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