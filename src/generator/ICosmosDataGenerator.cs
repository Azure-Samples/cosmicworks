// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator;

using Microsoft.Azure.Cosmos.Fluent;

using Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator.BuilderFactory;

/// <summary>
/// A data generator for Azure Cosmos DB for NoSQL.
/// </summary>
/// <typeparam name="T">
/// The type of data to generate.
/// </typeparam>
public interface ICosmosDataGenerator<T>
{
    /// <summary>
    /// Generate data in the Azure Cosmos DB for NoSQL container.
    /// </summary>
    /// <param name="factoryOptions">
    /// The options to use to create the <see cref="CosmosClientBuilder"/> instance.
    /// </param>
    /// <param name="databaseName">
    /// The name of the database.
    /// </param>
    /// <param name="containerName">
    /// The name of the container.
    /// </param>
    /// <param name="count">
    /// The number of items to generate.
    /// </param>
    /// <param name="disableHierarchicalPartitionKeys">
    /// A flag to indicate whether to disable hierarchical partition keys.
    /// </param>
    /// <param name="onItemCreate">
    /// The action to perform when an item is created.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    Task GenerateAsync(CosmosClientBuilderFactoryOptions factoryOptions, string databaseName, string containerName, int count, bool disableHierarchicalPartitionKeys, Action<string> onItemCreate);
}