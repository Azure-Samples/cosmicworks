// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Tool.Extensions;

/// <summary>
/// Extensions for the <see cref="IItemSettings{T}"/> interface.
/// </summary>
internal static class IItemSettingsExtensions
{
    public static IAnsiConsole GetConsole<T>(this IItemSettings<T> settings) where T : IItem
    {
        return AnsiConsole.Create(new AnsiConsoleSettings
        {
            Ansi = settings.DisableFormatting is false ? AnsiSupport.Detect : AnsiSupport.No,
            ColorSystem = settings.DisableFormatting is false ? ColorSystemSupport.Detect : ColorSystemSupport.NoColors
        });
    }
}