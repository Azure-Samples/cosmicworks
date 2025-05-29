// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Data.Models;

internal sealed record Thing(
    string Id,
    string Name,
    string Description,
    string CategoryName,
    string SubCategoryName,
    string ProductNumber,
    string? Color,
    string? Size,
    decimal Cost,
    decimal ListPrice,
    int Quantity,
    bool Clearance
)
{
    public Thing() : this(default!, default!, default!, default!, default!, default!, default!, default!, default!, default!, default!, default!)
    { }
}