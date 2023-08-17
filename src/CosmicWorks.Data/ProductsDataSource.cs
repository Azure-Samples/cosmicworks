using System.Text.Json;
using CosmicWorks.Data.Extensions;
using CosmicWorks.Data.Models;
using Raw = CosmicWorks.Data.Models.Raw;

namespace CosmicWorks.Data;

public sealed class ProductsDataSource : IDataSource<Product>
{
    public async Task<IReadOnlyList<Product>> GenerateAsync(int count = 1000)
    {
        int generatedEmployeesCount = count switch
        {
            > 1500 => throw new ArgumentOutOfRangeException(nameof(count), "You cannot generate more than 1,500 products."),
            < 1 => throw new ArgumentOutOfRangeException(nameof(count), "You must generate at least one product."),
            _ => count
        };

        using FileStream reader = File.OpenRead(Path.Combine("Source", @"things.json"));
        var results = await JsonSerializer.DeserializeAsync<IReadOnlyList<Raw.Thing>>(reader, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        });

        IEnumerable<Product> products = (results ?? Enumerable.Empty<Raw.Thing>()).OrderBy(i => Guid.NewGuid()).ToProducts();

        return products.Take(count).ToList();
    }
}