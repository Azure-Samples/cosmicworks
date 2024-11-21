// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Models;

/// <summary>
/// Represents a category.
/// </summary>
/// <param name="Name">
/// The name of the category.
/// </param>
/// <param name="SubCategory">
/// The associated sub-category.
/// </param>
public sealed record Category(
    string Name,
    SubCategory SubCategory
);