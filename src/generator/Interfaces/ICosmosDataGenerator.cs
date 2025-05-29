// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator.Interfaces;

/// <summary>
/// A data generator for Azure Cosmos DB for NoSQL.
/// </summary>
/// <typeparam name="T">
/// The type of data to generate. The type must implement the <see cref="IItem"/> interface.
/// </typeparam>
public interface ICosmosDataGenerator<T> where T : IItem
{
    /// <summary>
    /// Generate data in the Azure Cosmos DB for NoSQL container.
    /// </summary>
    /// <param name="options">
    /// The <see cref="ConnectionOptions"/> connection options to use to create the <see cref="CosmosClientBuilder"/> instance.
    /// </param>
    /// <param name="databaseName">
    /// The name of the database.
    /// </param>
    /// <param name="containerName">
    /// The name of the container.
    /// </param>
    /// <param name="count">
    /// The optinoal number of items to generate.
    /// If not specified, generate all items by default.
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
    Task GenerateAsync(ConnectionOptions options, string databaseName, string containerName, int? count, bool disableHierarchicalPartitionKeys, Action<T> onItemCreate);
}