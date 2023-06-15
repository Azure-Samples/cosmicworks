using System.Diagnostics.CodeAnalysis;
using CosmicWorks.Tool.Settings;
using Microsoft.Azure.Cosmos;
using Spectre.Console;
using Spectre.Console.Cli;
using Console = Spectre.Console.AnsiConsole;

namespace CosmicWorks.Tool.Commands;

public sealed class GenerateDataCommand : AsyncCommand<GenerateDataSettings>
{
    public override async Task<int> ExecuteAsync([NotNull] CommandContext context, [NotNull] GenerateDataSettings settings)
    {
        int returnValue = await Console.Status().StartAsync("Starting...", async ctx =>
        {
            await Task.Delay(1500);

            ctx.Status("Parsing connection string...");

            await Task.Delay(1500);

            Console.Write(
                new Rule("[bold teal]Client details[/]")
            );

            if (settings.ConnectionString is null)
            {
                Console.Write(
                    new Panel("[red]No connection string provided[/]")
                        .Header("Error")
                        .BorderColor(Color.Red)
                        .RoundedBorder()
                        .Expand()
                );
                Console.WriteLine();

                return 1;
            }
            

            Console.Write(
                new Panel(settings.ConnectionString)
                    .Header("Connection string")
                    .BorderColor(Color.Green)
                    .RoundedBorder()
                    .Expand()
            );
            Console.WriteLine();
            
            ctx.Status("Connecting to client...");

            CosmosClient client = new(settings.ConnectionString);

            Database database = await client.CreateDatabaseIfNotExistsAsync("cosmicworks");

            Console.Write(
                new Panel(database.Id)
                    .Header("Database")
                    .BorderColor(Color.Green)
                    .RoundedBorder()
                    .Expand()
            );
            Console.WriteLine();

            Container container = await database.CreateContainerIfNotExistsAsync("product", "/categoryId", throughput: 400);

            Console.Write(
                new Panel(container.Id)
                    .Header("Container")
                    .BorderColor(Color.Green)
                    .RoundedBorder()
                    .Expand()
            );
            Console.WriteLine();

            await Task.Delay(1500);

            return 0;
        });

        Console.WriteLine();

        return returnValue;
    }
}