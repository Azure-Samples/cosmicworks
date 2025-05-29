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

        services.AddTransient<IDataSource<Product>, ProductsDataSource>();
        services.AddTransient<IDataSource<Employee>, EmployeesDataSource>();

        services.AddSingleton<ICosmosClientService, CosmosClientService>();

        services.AddTransient<ICosmosDataService<Product>, CosmosDataService<Product>>();
        services.AddTransient<ICosmosDataService<Employee>, CosmosDataService<Employee>>();

        services.AddTransient<ICosmosDataGenerator<Product>, CosmosDataGenerator<Product>>();
        services.AddTransient<ICosmosDataGenerator<Employee>, CosmosDataGenerator<Employee>>();
    }
}