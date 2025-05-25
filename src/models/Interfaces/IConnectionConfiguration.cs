// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Models.Interfaces;

/// <summary>
/// Interface for types that represent the configuration for connecting to an Azure Cosmos DB for NoSQL account.
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

    /// <summary>
    /// Hides the credentials when rendering to output.
    /// </summary>
    bool? HideCredentials { get; }
}