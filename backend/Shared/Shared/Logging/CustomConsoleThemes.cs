using Serilog.Sinks.SystemConsole.Themes;

namespace Shared.Logging
{
    public static class CustomConsoleThemes
    {
        public static AnsiConsoleTheme SixteenEnhanced { get; } = new AnsiConsoleTheme(new Dictionary<ConsoleThemeStyle, string>
        {
            [ConsoleThemeStyle.Text] = "",
            [ConsoleThemeStyle.SecondaryText] = "\u001b[90m",
            [ConsoleThemeStyle.TertiaryText] = "\u001b[2m",

            [ConsoleThemeStyle.Invalid] = "\u001b[33;1m",
            [ConsoleThemeStyle.Null] = "\u001b[34m",
            [ConsoleThemeStyle.Name] = "\u001b[36;1m",
            [ConsoleThemeStyle.String] = "\u001b[36m",
            [ConsoleThemeStyle.Number] = "\u001b[34m",
            [ConsoleThemeStyle.Boolean] = "\u001b[34m",
            [ConsoleThemeStyle.Scalar] = "\u001b[32m",

            [ConsoleThemeStyle.LevelVerbose] = "\u001b[90m",
            [ConsoleThemeStyle.LevelDebug] = "\u001b[37m",
            [ConsoleThemeStyle.LevelInformation] = "\u001b[36;1m",
            [ConsoleThemeStyle.LevelWarning] = "\u001b[33;1m",
            [ConsoleThemeStyle.LevelError] = "\u001b[31;1m",
            [ConsoleThemeStyle.LevelFatal] = "\u001b[41;1;97m"
        });
    }
}
