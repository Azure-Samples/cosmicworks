// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator.Extensions;

/// <summary>
/// Extension methods to register services in the <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the generator services in the <see cref="IServiceCollection"/>.
    /// </summary>
    public static void RegisterGeneratorServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddSingleton<ICosmosDataService<Product>, CosmosDataService<Product>>();
        services.AddSingleton<ICosmosDataService<Employee>, CosmosDataService<Employee>>();
        services.AddSingleton<IDataSource<Product>, ProductsDataSource>();
        services.AddSingleton<IDataSource<Employee>, EmployeesDataSource>();
        services.AddSingleton<ICosmosDataGenerator<Product>, CosmosDataGenerator<Product>>();
        services.AddSingleton<ICosmosDataGenerator<Employee>, CosmosDataGenerator<Employee>>();
        services.AddSingleton<ICosmosClientService, CosmosClientService>();
    }
}