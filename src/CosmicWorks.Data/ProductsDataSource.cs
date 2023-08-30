using CosmicWorks.Data.Extensions;
using CosmicWorks.Data.Models;

namespace CosmicWorks.Data;

public sealed class ProductsDataSource : IDataSource<Product>
{
    public IReadOnlyList<Product> GetItems(int count = 1759)
    {
        int generatedProductsCount = count switch
        {
            > 1759 => throw new ArgumentOutOfRangeException(nameof(count), "You cannot generate more than 1,759 products."),
            < 1 => throw new ArgumentOutOfRangeException(nameof(count), "You must generate at least one product."),
            _ => count
        };

        IEnumerable<Product> products = Raw.Things.Get()
            .OrderBy(i => i.Id)
            .ToProducts();

        return products.Take(generatedProductsCount).ToList();
    }
}