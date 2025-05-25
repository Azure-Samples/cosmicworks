// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Data;

/// <summary>
/// A data source that generates items of type <see cref="Product"/>.
/// </summary>
public sealed class ProductsDataSource : IDataSource<Product>
{
    /// <summary>
    /// The maximum number of products that can be generated.
    /// </summary>
    public const int MaxProductsCount = 1759;

    /// <inheritdoc />
    public IReadOnlyList<Product> GetItems(int? count = MaxProductsCount)
    {
        int generatedProductsCount = count switch
        {
            null => MaxProductsCount,
            > MaxProductsCount => throw new ArgumentOutOfRangeException(nameof(count), $"You cannot generate more than {MaxProductsCount:N0} products."),
            < 1 => throw new ArgumentOutOfRangeException(nameof(count), "You must generate at least one product."),
            not null => count.Value
        };

        return [.. new Things()
            .OrderBy(i => i.Id)
            .Take(generatedProductsCount)
            .ToProducts()];
    }
}