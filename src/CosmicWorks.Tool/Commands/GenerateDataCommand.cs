using CosmicWorks.Data.Models;
using CosmicWorks.Generator;
using CosmicWorks.Tool.Settings;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Diagnostics.CodeAnalysis;
using Console = Spectre.Console.AnsiConsole;

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
        Console.WriteLine(); Console.Write(new Rule("[yellow]Parsing connection string[/]").LeftJustified().RuleStyle("olive")); Console.WriteLine();

        string connectionString = RetrieveConnectionString(settings.ConnectionString, settings.Emulator);

        Console.WriteLine(); Console.Write(new Rule("[yellow]Populating data[/]").LeftJustified().RuleStyle("olive")); Console.WriteLine();

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

            Console.Write(
                new Panel(grid)
                    .Header("[green bold]Employees configuration[/]")
                    .BorderColor(Color.White)
                    .RoundedBorder()
                    .Expand()
            );
            Console.WriteLine();

            await _employeeGenerator.GenerateAsync(
                connectionString: connectionString,
                databaseName: databaseName,
                containerName: containerName,
                count: settings.NumberOfEmployees,
                (output) => Console.MarkupLine($"[green][bold][[NEW]][/]\t{output}[/]")
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

            Console.Write(
                new Panel(grid)
                    .Header("[green]Products configuration[/]")
                    .BorderColor(Color.White)
                    .RoundedBorder()
                    .Expand()
            );
            Console.WriteLine();

            await _productGenerator.GenerateAsync(
                connectionString: connectionString,
                databaseName: databaseName,
                containerName: containerName,
                count: settings.NumberOfProducts,
                (output) => Console.MarkupLine($"[green][bold][[NEW]][/]\t{output}[/]")
            );
        }

        return 0;
    }

    private static string RetrieveConnectionString(string? connectionString, bool emulator)
    {
        if (emulator)
        {
            connectionString = "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        }
        else
        {
            connectionString ??= Console.Prompt(
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

        Console.Write(
            new Panel($"[teal]{connectionString}[/]")
                .Header("[green]Connection string[/]")
                .BorderColor(Color.White)
                .RoundedBorder()
                .Expand()
        );

        return connectionString;
    }
}