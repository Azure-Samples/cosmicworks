// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Data.Extensions;

internal static class ProductExtensions
{
    public static IEnumerable<Product> ToProducts(this IEnumerable<Thing> items) =>
        items.Select(i => new Product(
                Id: i.Id,
                Name: i.Name,
                Description: i.Description,
                Category:  i.CategoryName,
                SubCategory: i.SubCategoryName,
                SKU: i.ProductNumber,
                Tags: [.. i.GetTags()],
                Cost: i.Cost,
                Price: i.ListPrice,
                Quantity: i.Quantity,
                Clearance: i.Clearance
            )
        );

    public static IEnumerable<string> GetTags(this Thing thing)
    {
        foreach (var value in new List<string> { thing.CategoryName, thing.SubCategoryName, thing.Color!, thing.Size! })
        {
            if (value is not null)
            {
                yield return value;
            }
        }
    }
}