// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator.Interfaces;

/// <summary>
/// An interface for creating instances of <see cref="CosmosClient"/>.
/// </summary>
public interface ICosmosClientService
{
    /// <summary>
    /// Creates a new instance of the <see cref="CosmosClient"/> class.
    /// Uses built-in retry policies and bulk execution configuration.
    /// </summary>
    /// <param name="options">
    /// The <see cref="ConnectionOptions"/> connection options to use to create the <see cref="CosmosClientBuilder"/> instance that eventually generates the <see cref="CosmosClient"/> return value.
    /// </param>
    /// <returns>
    /// An instance of type <see cref="CosmosClient"/> that is configured with the provided <see cref="ConnectionOptions"/>.
    /// </returns>
    CosmosClient GetCosmosClient(ConnectionOptions options);
}