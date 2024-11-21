// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator.BuilderFactory;

using Microsoft.Azure.Cosmos.Fluent;

/// <summary>
/// Options for creating a <see cref="CosmosClientBuilder"/> instance.
/// </summary>
public record CosmosClientBuilderFactoryOptions
{
    /// <summary>
    /// Gets the connection string to use.
    /// </summary>
    public required string? ConnectionString { get; init; }

    /// <summary>
    /// Gets the endpoint to use.
    /// </summary>
    public required string? Endpoint { get; init; }

    /// <summary>
    /// Gets a value indicating whether to use the emulator.
    /// </summary>
    public required bool UseEmulator { get; init; }

    /// <summary>
    /// Gets a value indicating whether to use role-based access control.
    /// </summary>
    public required bool UseRoleBasedAccessControl { get; init; }
}