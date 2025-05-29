// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Tool.Interfaces;

/// <summary>
/// Interface for types that represent the command-line interface (CLI) settings for generating items in Azure Cosmos DB for NoSQL.
/// </summary>
/// <typeparam name="T"></typeparam>
internal interface IItemSettings<T> where T : IItem
{
    /// <summary>
    /// The name of the database to use.
    /// </summary>
    string DatabaseName { get; }

    /// <summary>
    /// The name of the container to use.
    /// </summary>
    string ContainerName { get; }

    /// <summary>
    /// The number of items to generate.
    /// </summary>
    int? Quantity { get; }

    /// <summary>
    /// Disables ANSI and color formatting for console output.
    /// </summary>
    bool? DisableFormatting { get; }

    /// <summary>
    /// Disables hierarchical partition keys and uses only the first partition key value.
    /// </summary>
    bool? DisableHierarchicalPartitionKeys { get; }
}