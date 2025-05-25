// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Tool.Extensions;

internal static class GenerateSettingsExtensions
{
    public static IAnsiConsole GetConsole(this GenerateSettings settings)
    {
        return AnsiConsole.Create(new AnsiConsoleSettings
        {
            Ansi = settings.DisableFormatting is false ? AnsiSupport.Detect : AnsiSupport.No,
            ColorSystem = settings.DisableFormatting is false ? ColorSystemSupport.Detect : ColorSystemSupport.NoColors
        });
    }

    public static string? GetCredential(this GenerateSettings settings)
    {
        return settings.Emulator ? "<emulator>" : settings.Endpoint ?? settings.ConnectionString;
    }
}