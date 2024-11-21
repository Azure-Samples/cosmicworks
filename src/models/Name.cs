// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Models;

/// <summary>
/// Represents a full name.
/// </summary>  
/// <param name="First">
/// The first name.
/// </param>
/// <param name="Last">
/// The last name.
/// </param>
public sealed record Name(
    string First,
    string Last
);
