// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator.DataSource;

/// <summary>
/// A context for interacting with Azure Cosmos DB for NoSQL.
/// </summary>
public interface ICosmosContext
{
    /// <summary>
    /// Seeds data asynchronously.
    /// </summary>
    /// <typeparam name="T">
    /// The type of items to seed.
    /// </typeparam>
    /// <param name="factoryOptions">
    /// The options to use to create the <see cref="CosmosClientBuilder"/> instance.
    /// </param>
    /// <param name="databaseName">
    /// The name of the database.
    /// </param>
    /// <param name="containerProperties">
    /// The properties of the container.
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
    Task SeedDataAsync<T>(CosmosClientBuilderFactoryOptions factoryOptions, string databaseName, ContainerProperties containerProperties, IEnumerable<T> items, Action<string> onCreated);
}