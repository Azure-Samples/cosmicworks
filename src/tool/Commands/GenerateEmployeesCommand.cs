// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Tool.Commands;

internal sealed class GenerateEmployeesCommand(
    ICosmosDataGenerator<Employee> employeeGenerator
) : AsyncCommand<GenerateEmployeesSettings>
{
    /// <inheritdoc />
    public override async Task<int> ExecuteAsync(CommandContext context, GenerateEmployeesSettings settings)
    {
        IAnsiConsole console = settings.GetConsole();

        console.WriteConfiguration(settings);

        ConnectionOptions options = settings.ToConnectionOptions();

        await employeeGenerator.GenerateAsync(
            options: options,
            databaseName: settings.DatabaseName,
            containerName: settings.ContainerName,
            count: settings.Quantity,
            disableHierarchicalPartitionKeys: settings.DisableHierarchicalPartitionKeys ?? false,
            onItemCreate: console.WriteItem
        );

        return 0;
    }
}