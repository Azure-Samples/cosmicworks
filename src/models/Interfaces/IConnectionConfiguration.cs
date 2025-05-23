// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Models.Interfaces;

/// <summary>
/// Represents an item in Azure Cosmos DB for NoSQL.
/// </summary>
public interface IConnectionConfiguration
{
    /// <summary>
    /// The connection string to the Azure Cosmos DB for NoSQL account using a resource owner password credential (ROPC).
    /// </summary>
    string? ConnectionString { get; }

    /// <summary>
    /// Use the Azure Cosmos DB Emulator.
    /// </summary>
    bool Emulator { get; }

    /// <summary>
    /// The endpoint to the Azure Cosmos DB for NoSQL account.
    /// </summary>
    string? Endpoint { get; }
}