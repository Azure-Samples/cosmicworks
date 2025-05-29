// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Models;

/// <summary>
/// Options for creating a connection.
/// </summary>
public record ConnectionOptions
{
    /// <summary>
    /// The credential to use.
    /// </summary>
    public required string Credential { get; init; }

    /// <summary>
    /// Gets the endpoint to use.
    /// </summary>
    public required ConnectionType Type { get; init; }

    /// <summary>
    /// Indicates whether the emulator is in use.
    /// </summary>
    public required bool Emulator { get; init; }
}