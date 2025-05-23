// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Tool.Extensions;

internal static class AnsiConsoleExtensions
{
    public static void WriteItem<T>(this IAnsiConsole console, T item)
    {
        console.MarkupLine($"[green][bold][[SEED]][/]\t{item}[/]");
    }

    public static void WriteConfiguration(this IAnsiConsole console, GenerateSettings settings)
    {
        console.Write(new Rule("[yellow]Parsing connection string[/]").LeftJustified().RuleStyle("olive"));

        string? credential = settings.GetCredential();

        if (credential is not null)
        {
            console.Write(
                new Panel(settings.HideCredentials is false ? $"[teal]{credential}[/]" : $"[teal dim]{new String('*', credential.Length)}[/]")
                    .Header("[green]Credential[/]")
                    .BorderColor(Color.White)
                    .RoundedBorder()
                    .Expand()
            );
        }

        Grid grid = new();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddRow("[green bold]Database[/]", $"[teal]{settings.DatabaseName}[/]");
        grid.AddRow("[green bold]Container[/]", $"[teal]{settings.ContainerName}[/]");
        grid.AddRow("[green]Count[/]", $"[teal]{settings.Quantity:##,#}[/]");

        console.Write(
            new Panel(grid)
                .Header("[green]Products configuration[/]")
                .BorderColor(Color.White)
                .RoundedBorder()
                .Expand()
        );
    }
}