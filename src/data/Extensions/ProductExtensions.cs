// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Data.Extensions;

using Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Models;

internal static class ProductExtensions
{
    public static IEnumerable<Product> ToProducts(this IEnumerable<Raw.Thing> items) =>
        items.Select(i => new Product(
                Id: $"{i.Id:00000000-0000-0000-0000-000000000000}",
                Name: i.Name,
                Description: i.Description,
                Category: new Category(
                    i.CategoryName,
                    new SubCategory(i.SubCategoryName)
                ),
                SKU: i.ProductNumber,
                Tags: [.. i.GetTags()],
                Cost: i.Cost,
                Price: i.ListPrice
            )
        );

    public static IEnumerable<string> GetTags(this Raw.Thing thing)
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