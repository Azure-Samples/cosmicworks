// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Models;

/// <summary>
/// Represents an address.
/// </summary>
/// <param name="Name">
/// The name of the address.
/// </param>
/// <param name="Street">
/// The street of the address.
/// </param>
/// <param name="City">
/// The city of the address.
/// </param>
/// <param name="State">
/// The state of the address.
/// </param>
/// <param name="ZipCode">
/// The zip code of the address.
/// </param>
/// <param name="Type">
/// The type of the address. Default is "work".
/// </param>
public sealed record Address(
    string Name,
    string Street,
    string City,
    string State,
    string ZipCode,
    string Type = "work"
);