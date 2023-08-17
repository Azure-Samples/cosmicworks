using System.Diagnostics.CodeAnalysis;
using CosmicWorks.Tool.Interfaces;
using CosmicWorks.Tool.Models;
using CosmicWorks.Tool.Settings;
using Microsoft.Azure.Cosmos;
using Spectre.Console;
using Spectre.Console.Cli;
using Console = Spectre.Console.AnsiConsole;

namespace CosmicWorks.Tool.Commands;

public sealed class GenerateDataCommand : AsyncCommand<GenerateDataSettings>
{
    private readonly IGeneratorService<Product> _productGeneratorService;

    public GenerateDataCommand(IGeneratorService<Product> productGeneratorService)
    {
        _productGeneratorService = productGeneratorService;
    }

    public override async Task<int> ExecuteAsync([NotNull] CommandContext context, [NotNull] GenerateDataSettings settings)
    {
        Console.WriteLine(); Console.Write(new Rule("[yellow]Parsing connection string[/]").LeftJustified().RuleStyle("olive")); Console.WriteLine();

        string connectionString = RetrieveConnectionString(settings.ConnectionString, settings.Emulator);

        Console.WriteLine(); Console.Write(new Rule("[yellow]Connecting to client[/]").LeftJustified().RuleStyle("olive")); Console.WriteLine(); Console.WriteLine();

        Container container = await RetrieveContainerAsync(connectionString, settings.NumberOfItems);

        Console.WriteLine(); Console.Write(new Rule("[yellow]Populating data[/]").LeftJustified().RuleStyle("olive")); Console.WriteLine();

        await PopulateDataAsync(_productGeneratorService, container, settings.NumberOfItems);

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

    private static async Task<Container> RetrieveContainerAsync(string connectionString, int count)
    {
        Container? container = default;

        var tree = new Tree(String.Empty)
            .Style("green");

        await Console.Live(tree)
            .AutoClear(false)
            .Cropping(VerticalOverflowCropping.Top)
            .StartAsync(async context =>
            {
                CosmosSerializationOptions serializerOptions = new()
                {
                    IgnoreNullValues = true,
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                };
                CosmosClientOptions options = new()
                {
                    AllowBulkExecution = true,
                    SerializerOptions = serializerOptions,
                    MaxRetryAttemptsOnRateLimitedRequests = count
                };
                CosmosClient client = new(connectionString, options);

                AccountProperties accountProperties = await client.ReadAccountAsync();

                var rootNode = tree.AddNode($"[green]Account:[/] [teal]{accountProperties.Id}[/]");
                context.Refresh();

                DatabaseProperties databaseProperties = new(
                    id: "cosmicworks"
                );
                Database database = await client.CreateDatabaseIfNotExistsAsync(databaseProperties.Id);

                var databaseNode = rootNode.AddNode($"[green]Database:[/] [teal]{database.Id}[/]");
                context.Refresh();

                ContainerProperties containerProperties = new(
                    id: "product",
                    partitionKeyPaths: new List<string> { "/tenantId", "/vendorId", "/categoryId" }
                );
                container = await database.CreateContainerIfNotExistsAsync(containerProperties, throughput: 400);

                var containerNode = databaseNode.AddNode($"[green]Container:[/] [teal]{container.Id}[/]");
                context.Refresh();
            }
        );

        if (container is null)
        {
            throw new InvalidOperationException("Unable to retrieve container.");
        }

        return container;
    }

    private static async Task PopulateDataAsync(IGeneratorService<Product> productGeneratorService, Container container, int count)
    {
        var grid = new Grid();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddRow("[green]Count[/]", $"[teal]{count:##,#}[/]");

        Console.Write(
            new Panel(grid)
                .Header("[green]Configuration[/]")
                .BorderColor(Color.White)
                .RoundedBorder()
                .Expand()
        );

        var products = await productGeneratorService.GenerateDataAsync(count);

        await Console.Progress()
            .AutoRefresh(true)
            .AutoClear(false)
            .StartAsync(async context =>
                {
                    var progressTask = context.AddTask("[green]Adding products[/]");
                    progressTask.MaxValue(products.Count);
                    List<Task> tasks = new(products.Count);
                    foreach (var product in products)
                    {
                        tasks.Add(
                            container.UpsertItemAsync(
                                item: product,
                                partitionKey: new PartitionKeyBuilder()
                                    .Add(product.TenantId)
                                    .Add(product.VendorId)
                                    .Add(product.CategoryId)
                                    .Build()
                            ).ContinueWith(response =>
                            {
                                if (response.IsCompletedSuccessfully)
                                {
                                    progressTask.Increment(1);
                                }
                                else
                                {
                                    AggregateException? aggregateException = response?.Exception?.Flatten();
                                    if (aggregateException is not null)
                                    {
                                        if (aggregateException.InnerExceptions.FirstOrDefault(innerEx => innerEx is CosmosException) is CosmosException cosmosException)
                                        {
                                            Console.MarkupLine($"[bold red]Received {cosmosException.StatusCode} ({cosmosException.Message}).[/]");
                                        }
                                        else
                                        {
                                            Console.MarkupLine($"[bold red]Exception {aggregateException.InnerExceptions.FirstOrDefault()}.[/]");
                                        }
                                    }
                                }
                            })
                        );
                    }
                    await Task.WhenAll(tasks);
                }
        );
    }
}