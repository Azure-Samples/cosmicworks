// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Models;

/// <summary>
/// Represents a product.
/// </summary>
/// <param name="Id">
/// The unique identifier of the product.
/// </param>
/// <param name="Name">
/// The name of the product.
/// </param>
/// <param name="Description">
/// The description for the product.
/// </param>
/// <param name="Category">
/// The category of the product.
/// </param>
/// <param name="SubCategory">
/// The sub-category of the product associated with the category.
/// </param>
/// <param name="SKU">
/// The stock keeping unit (SKU) for the product.
/// </param>
/// <param name="Tags">
/// The set of metadata tags associated with the product.
/// </param>
/// <param name="Cost">
/// The cost of the product.
/// </param>
/// <param name="Price">
/// The sale price of the product.
/// </param>
/// <param name="Quantity">
/// The quantity of the product in stock.
/// </param>
/// <param name="Clearance">
/// Indicates if the product is on clearance.
/// </param>
/// <param name="Type">
/// The type of the item. Default is "Product".
/// </param>
public sealed record Product(
    string Id,
    string Name,
    string Description,
    string Category,
    string SubCategory,
    string SKU,
    IList<string> Tags,
    decimal Cost,
    decimal Price,
    int Quantity,
    bool Clearance,
    string Type = nameof(Product)
) : IItem
{
    /// <summary>
    /// Outputs the product as a string.
    /// </summary>
    /// <returns>
    /// A string containing the product's unique identifier, name, and category.
    /// </returns>
    public override string ToString() => $"{Id} | {Name} - {Category}";

    /// <inheritdoc />
    public IReadOnlyList<string> PartitionKeys =>
    [
        Category,
        SubCategory,
    ];
}