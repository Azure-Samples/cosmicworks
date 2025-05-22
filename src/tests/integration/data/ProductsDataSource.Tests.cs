// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Data.Tests.Integration;

public class ProductsDataSourceTests
{
    [Fact]
    public void GetAllProductsTest()
    {
        // Arrange
        ProductsDataSource source = new();

        // Act
        IReadOnlyList<Product> result = source.GetItems();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.All(result, Assert.NotNull);
        Assert.Equal(1759, result.Count);
    }

    [Fact]
    public void GetSomeProductsTest()
    {
        // Arrange
        ProductsDataSource source = new();

        // Act
        IReadOnlyList<Product> result = source.GetItems(count: 100);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.All(result, Assert.NotNull);
        Assert.Equal(100, result.Count);
    }
    [Fact]
    public void GetValidProductTest()
    {
        // Arrange
        ProductsDataSource source = new();

        // Act
        Product result = source.GetItems(count: 1).Single();

        // Assert
        Assert.NotNull(result);
        Assert.False(string.IsNullOrEmpty(result.Id));
        Assert.False(string.IsNullOrEmpty(result.Name));
        Assert.False(string.IsNullOrEmpty(result.Description));
        Assert.False(string.IsNullOrEmpty(result.Category));
        Assert.False(string.IsNullOrEmpty(result.SubCategory));
        Assert.False(string.IsNullOrEmpty(result.SKU));
    }
}