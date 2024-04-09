using CosmicWorks.Models;
using CosmicWorks.Generator;
using CosmicWorks.Tool.Settings;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace CosmicWorks.Tool.Commands;

public sealed class GenerateDataCommand : AsyncCommand<GenerateDataSettings>
{
    private readonly ICosmosDataGenerator<Product> _productGenerator;
    private readonly ICosmosDataGenerator<Employee> _employeeGenerator;

    public GenerateDataCommand(ICosmosDataGenerator<Product> productGenerator, ICosmosDataGenerator<Employee> employeeGenerator)
    {
        _productGenerator = productGenerator;
        _employeeGenerator = employeeGenerator;
    }

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

        string? connectionString = settings.ConnectionString;
        if (settings.Emulator)
        {
            connectionString = "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        }
        else
        {
            connectionString ??= ansiConsole.Prompt(
                new TextPrompt<string>("What is your [teal]connection string[/]?")
                    .PromptStyle("teal")
                    .ValidationErrorMessage("[bold red]That's not a valid connection string[/].")
                    .Validate(c =>
                    {
                        return String.IsNullOrWhiteSpace(c) switch
                        {
                            true => ValidationResult.Error("[bold red]You must provide a connection string[/]"),
                            false => ValidationResult.Success(),
                        };
                    })
            );
        }

        ansiConsole.Write(
            new Panel(settings.HideCredentials is false ? $"[teal]{connectionString}[/]" : $"[teal dim]{new String('*', connectionString.Length)}[/]")
                .Header("[green]Connection string[/]")
                .BorderColor(Color.White)
                .RoundedBorder()
                .Expand()
        );

        ansiConsole.Write(new Rule("[yellow]Populating data[/]").LeftJustified().RuleStyle("olive"));

        string databaseName = "cosmicworks";

        if (settings.NumberOfEmployees > 0)
        {
            string containerName = "employees";

            var grid = new Grid();
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

            await _employeeGenerator.GenerateAsync(
                connectionString: connectionString,
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

            var grid = new Grid();
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

            await _productGenerator.GenerateAsync(
                connectionString: connectionString,
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