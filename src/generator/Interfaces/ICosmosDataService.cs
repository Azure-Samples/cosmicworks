// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator.Interfaces;

/// <summary>
/// A context for interacting with Azure Cosmos DB for NoSQL.
/// </summary>
/// <typeparam name="T">
/// The type of data in the container. The type must implement the <see cref="IItem"/> interface.
/// </typeparam>
public interface ICosmosDataService<T> where T : IItem
{
    /// <summary>
    /// Seeds data asynchronously.
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
    /// <param name="items">
    /// The items to seed.
    /// </param>
    /// <param name="onCreated">
    /// The action to perform when the data is created.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    Task SeedDataAsync(ConnectionOptions options, string databaseName, string containerName, IEnumerable<T> items, Action<T> onCreated);

    /// <summary>
    /// Generates resources asynchronously if they do not already exist.
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
    /// <param name="partitionKeys">
    /// The partition keys to use if creating a new container.
    /// </param>
    /// <param name="disableHierarchicalPartitionKeys">
    /// A value indicating whether to disable hierarchical partition keys.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation with a boolean result.
    /// The boolean indicates whether the account is serverless or not.
    /// </returns>
    /// <remarks>
    /// This method should only be used when the <see cref="ConnectionType"/> is <see cref="ConnectionType.ResourceOwnerPasswordCredential"/>.
    /// The method will check the account to determine if it is serverless and provision throghput at 400 request units per second (RU/s) per database if it is not.
    /// </remarks>
    Task<bool> ProvisionResourcesAsync(ConnectionOptions options, string databaseName, string containerName, string[] partitionKeys, bool disableHierarchicalPartitionKeys);

    /// <summary>
    /// Updates the throughput of the database asynchronously.
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
    /// <param name="throughputs">
    /// The set of throughput values to attempt when updating the database.
    /// </param>
    /// <returns></returns>
    Task UpdateThroughputAsync(ConnectionOptions options, string databaseName, string containerName, int[] throughputs);
}