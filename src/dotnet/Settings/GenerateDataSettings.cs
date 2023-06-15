using System.ComponentModel;
using Spectre.Console.Cli;

namespace CosmicWorks.Tool.Settings;

public sealed class GenerateDataSettings : CommandSettings
{
    [Description("The connection string to the Azure Cosmos DB for NoSQL account.")]
    [CommandOption("-c|--connection-string <CONNECTION_STRING>")]
    public string? ConnectionString { get; init; }
}