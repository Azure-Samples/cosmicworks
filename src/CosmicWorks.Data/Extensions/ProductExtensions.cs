using Bogus;
using CosmicWorks.Data.Models;
using Raw = CosmicWorks.Data.Models.Raw;

namespace CosmicWorks.Data.Extensions;

internal static class ProductExtensions
{
    private static readonly Faker faker = new("en");

    public static IEnumerable<Product> ToProducts(this IEnumerable<Raw.Thing> items) =>
    items.Select(i => new Product(
            Name: i.Name,
            Description: i.Description,
            Category: new Category(
                i.CategoryName,
                new SubCategory(i.SubCategoryName)
            ),
            SKU: i.ProductNumber,
            Tags: i.GetTags().ToList(),
            Price: Decimal.Round(faker.Random.Decimal(min: 1, max: 1000), 2, MidpointRounding.AwayFromZero)
        )
    );

    public static IEnumerable<string> GetTags(this Raw.Thing thing)
    {
        foreach (var value in new List<string> { thing.CategoryName, thing.SubCategoryName, thing.Color, thing.Size })
        {
            if (value is not null)
            {
                yield return value;
            }
        }
    }
}