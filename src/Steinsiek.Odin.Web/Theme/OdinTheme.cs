namespace Steinsiek.Odin.Web.Theme;

/// <summary>
/// Defines the custom MudBlazor theme for the Odin E-Commerce frontend.
/// </summary>
public static class OdinTheme
{
    /// <summary>
    /// The application's custom MudTheme with dark and light palettes.
    /// </summary>
    public static readonly MudTheme Theme = new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#6C5CE7",
            PrimaryDarken = "#5A4BD1",
            PrimaryLighten = "#A29BFE",
            Secondary = "#00CEC9",
            SecondaryDarken = "#00B5B0",
            SecondaryLighten = "#55EFC4",
            Tertiary = "#FD79A8",
            Background = "#FAFAFA",
            Surface = "#FFFFFF",
            AppbarBackground = "#FFFFFF",
            AppbarText = "#2D3436",
            DrawerBackground = "#FFFFFF",
            DrawerText = "#2D3436",
            TextPrimary = "#2D3436",
            TextSecondary = "#636E72",
            ActionDefault = "#636E72",
            Success = "#00B894",
            Warning = "#FDCB6E",
            Error = "#E17055",
            Info = "#74B9FF"
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#A29BFE",
            PrimaryDarken = "#6C5CE7",
            PrimaryLighten = "#D0CCFF",
            Secondary = "#55EFC4",
            SecondaryDarken = "#00CEC9",
            SecondaryLighten = "#81ECEC",
            Tertiary = "#FD79A8",
            Background = "#0D1117",
            Surface = "#161B22",
            AppbarBackground = "#161B22",
            AppbarText = "#F0F0F0",
            DrawerBackground = "#0D1117",
            DrawerText = "#F0F0F0",
            TextPrimary = "#F0F0F0",
            TextSecondary = "#8B949E",
            ActionDefault = "#8B949E",
            Success = "#00B894",
            Warning = "#FDCB6E",
            Error = "#E17055",
            Info = "#74B9FF"
        },
        Typography = new Typography
        {
            Default = new DefaultTypography
            {
                FontFamily = ["Inter", "Segoe UI", "Roboto", "Helvetica Neue", "Arial", "sans-serif"],
                FontSize = "0.9375rem",
                FontWeight = "400",
                LineHeight = "1.6"
            },
            H1 = new H1Typography
            {
                FontSize = "2.5rem",
                FontWeight = "700",
                LineHeight = "1.2"
            },
            H2 = new H2Typography
            {
                FontSize = "2rem",
                FontWeight = "700",
                LineHeight = "1.3"
            },
            H3 = new H3Typography
            {
                FontSize = "1.5rem",
                FontWeight = "600",
                LineHeight = "1.4"
            }
        },
        LayoutProperties = new LayoutProperties
        {
            DefaultBorderRadius = "12px",
            DrawerWidthLeft = "280px"
        }
    };
}
