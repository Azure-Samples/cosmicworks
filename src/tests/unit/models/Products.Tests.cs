// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Models.Tests.Unit;

public class ProductsTests
{
    [Fact]
    public void ToStringTest()
    {
        // Arrange
        Product target = new(
            Id: "00000000-0000-0000-0000-000000000000",
            Name: "name",
            Description: string.Empty,
            Category: "category",
            SubCategory: string.Empty,
            SKU: string.Empty,
            Tags: [],
            Cost: default,
            Price: default,
            Quantity: default,
            Clearance: default
        );

        // Act
        string actual = target.ToString();

        // Assert
        Assert.Equal("00000000-0000-0000-0000-000000000000 | name - category", actual);
    }
}