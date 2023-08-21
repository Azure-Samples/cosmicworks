using CosmicWorks.Data.Extensions;
using CosmicWorks.Data.Models;

namespace CosmicWorks.Data;

public sealed class ProductsDataSource : IDataSource<Product>
{
    public async Task<IReadOnlyList<Product>> GetItemsAsync(int count = 1000)
    {
        int generatedEmployeesCount = count switch
        {
            > 1500 => throw new ArgumentOutOfRangeException(nameof(count), "You cannot generate more than 1,500 products."),
            < 1 => throw new ArgumentOutOfRangeException(nameof(count), "You must generate at least one product."),
            _ => count
        };

        await Task.Delay(TimeSpan.FromSeconds(1));

        IEnumerable<Product> products = Raw.Things.Get()
            .OrderBy(i => Guid.NewGuid())
            .ToProducts();

        return products.Take(count).ToList();
    }
}