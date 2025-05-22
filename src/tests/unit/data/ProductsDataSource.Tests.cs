// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Data.Tests.Unit;

public class ProductsDataSourceTests
{
    [Fact]
    public void ZeroCountTest()
    {
        // Arrange
        ProductsDataSource source = new();

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            source.GetItems(count: 0);
        });
    }

    [Fact]
    public void TooLargeCountTest()
    {
        // Arrange
        ProductsDataSource source = new();

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            source.GetItems(count: 1760);
        });
    }
}