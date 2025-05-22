// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Tool.Commands;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;

using Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator;
using Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator.BuilderFactory;
using Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Models;
using Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Tool.Settings;

using Spectre.Console;
using Spectre.Console.Cli;

/// <summary>
/// A command to generate data.
/// </summary>
/// <param name="productGenerator">
/// The generator for products.
/// </param>
/// <param name="employeeGenerator">
/// The generator for employees.
/// </param>
internal sealed class GenerateDataCommand(
    ICosmosDataGenerator<Product> productGenerator,
    ICosmosDataGenerator<Employee> employeeGenerator) : AsyncCommand<GenerateDataSettings>
{
    /// <summary>
    /// Executes the command asynchronously.
    /// </summary>
    public override async Task<int> ExecuteAsync([NotNull] CommandContext context, [NotNull] GenerateDataSettings settings)
    {
        AnsiConsoleSettings consoleSettings = new()
        {
            Ansi = settings.DisableFormatting is false ? AnsiSupport.Detect : AnsiSupport.No,
            ColorSystem = settings.DisableFormatting is false ? ColorSystemSupport.Detect : ColorSystemSupport.NoColors
        };
        IAnsiConsole ansiConsole = AnsiConsole.Create(consoleSettings);

        if (settings.RenderVersion)
        {
            string? version = Assembly.GetExecutingAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
            ansiConsole.MarkupLine($"[teal][bold][[VERSION]][/]\t{version}[/]");
            return 0;
        }

        ansiConsole.Write(new Rule("[yellow]Parsing connection string[/]").LeftJustified().RuleStyle("olive"));

        string? credential = settings.Emulator ? "<emulator>" : settings.RoleBasedAccessControl ? settings.Endpoint : settings.ConnectionString;

        if (credential is not null)
        {
            ansiConsole.Write(
                new Panel(settings.HideCredentials is false ? $"[teal]{credential}[/]" : $"[teal dim]{new String('*', credential.Length)}[/]")
                    .Header("[green]Credential[/]")
                    .BorderColor(Color.White)
                    .RoundedBorder()
                    .Expand()
            );
        }

        ansiConsole.Write(new Rule("[yellow]Populating data[/]").LeftJustified().RuleStyle("olive"));

        string databaseName = "cosmicworks";

        CosmosClientBuilderFactoryOptions factoryOptions = new()
        {
            ConnectionString = settings.ConnectionString,
            Endpoint = settings.Endpoint,
            UseEmulator = settings.Emulator,
            UseRoleBasedAccessControl = settings.RoleBasedAccessControl,
        };

        if (settings.NumberOfEmployees > 0)
        {
            string containerName = "employees";

            Grid grid = new();
            grid.AddColumn();
            grid.AddColumn();
            grid.AddRow("[green bold]Database[/]", $"[teal]{databaseName}[/]");
            grid.AddRow("[green bold]Container[/]", $"[teal]{containerName}[/]");
            grid.AddRow("[green bold]Count[/]", $"[teal]{settings.NumberOfEmployees:##,#}[/]");

            ansiConsole.Write(
                new Panel(grid)
                    .Header("[green bold]Employees configuration[/]")
                    .BorderColor(Color.White)
                    .RoundedBorder()
                    .Expand()
            );

            ansiConsole.MarkupLine("[grey dim italic][bold]Warning[/]: Items are generated in parallel and the order of output logs may differ between runs.[/]");

            await employeeGenerator.GenerateAsync(
                factoryOptions: factoryOptions,
                databaseName: databaseName,
                containerName: containerName,
                count: settings.NumberOfEmployees,
                disableHierarchicalPartitionKeys: settings.DisableHierarchicalPartitionKeys ?? false,
                onItemCreate: (output) => ansiConsole.MarkupLine($"[green][bold][[SEED]][/]\t{output}[/]")
            );
        }

        if (settings.NumberOfProducts > 0)
        {
            string containerName = "products";

            Grid grid = new();
            grid.AddColumn();
            grid.AddColumn();
            grid.AddRow("[green bold]Database[/]", $"[teal]{databaseName}[/]");
            grid.AddRow("[green bold]Container[/]", $"[teal]{containerName}[/]");
            grid.AddRow("[green]Count[/]", $"[teal]{settings.NumberOfProducts:##,#}[/]");

            ansiConsole.Write(
                new Panel(grid)
                    .Header("[green]Products configuration[/]")
                    .BorderColor(Color.White)
                    .RoundedBorder()
                    .Expand()
            );

            await productGenerator.GenerateAsync(
                factoryOptions: factoryOptions,
                databaseName: databaseName,
                containerName: containerName,
                count: settings.NumberOfProducts,
                disableHierarchicalPartitionKeys: settings.DisableHierarchicalPartitionKeys ?? false,
                onItemCreate: (output) => ansiConsole.MarkupLine($"[green][bold][[SEED]][/]\t{output}[/]")
            );
        }

        return 0;
    }
}