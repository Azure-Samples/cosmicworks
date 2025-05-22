// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator.BuilderFactory;

/// <summary>
/// A factory for creating <see cref="CosmosClientBuilder"/> instances.
/// </summary>
public interface ICosmosClientBuilderFactory
{
    /// <summary>
    /// Gets a <see cref="CosmosClientBuilder"/> instance.
    /// </summary>
    /// <param name="options">
    /// The options to use when creating the <see cref="CosmosClientBuilder"/> instance.
    /// </param>
    /// <returns>
    /// A <see cref="CosmosClientBuilder"/> instance.
    /// </returns>
    CosmosClientBuilder GetBuilder(CosmosClientBuilderFactoryOptions options);
}