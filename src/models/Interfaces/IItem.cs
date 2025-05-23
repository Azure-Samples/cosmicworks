// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Models.Interfaces;

/// <summary>
/// Represents an item in Azure Cosmos DB for NoSQL.
/// </summary>
public interface IItem
{
    /// <summary>
    /// Gets the unique identifier of the item.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the type of the item.
    /// </summary>
    string Type { get; }

    /// <summary>
    /// Gets the requested partition keys for this type.
    /// </summary>
    /// <remarks>
    /// This is used to generate the partition key paths if the container is automatically created.
    /// </remarks>
    IReadOnlyList<string> PartitionKeys =>
    [
        Id
    ];
}