// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Data.Tests.Unit;

public class ProductExtensionsTests
{
    [Fact]
    public void NullInputTest()
    {
        // Arrange
        IEnumerable<Thing> input = default!;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            input.ToProducts()
        );
    }

    [Fact]
    public void NullItemInputTest()
    {
        // Arrange
        List<Thing> input = [
            default!
        ];

        // Act & Assert
        Assert.Throws<NullReferenceException>(() =>
        {
            IEnumerable<Product> actual = input.ToProducts();
            actual.Count();
        });
    }

    [Fact]
    public void EmptyInputTest()
    {
        // Arrange
        List<Thing> input = [];

        // Act
        IEnumerable<Product> actual = input.ToProducts();

        // Assert
        Assert.Empty(actual);
    }

    [Fact]
    public void InputSameCountTest()
    {
        // Arrange
        List<Thing> input = [
            new Thing(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, default, default, default, default, default, default)
        ];

        // Act
        IEnumerable<Product> actual = input.ToProducts();

        // Assert
        Assert.Equal(input.Count, actual.Count());
    }

    [Fact]
    public void ThingToProductTest()
    {
        // Arrange
        List<Thing> input = [
            new()
            {
                Id = "00000000-0000-0000-0000-000000000000",
                Name = "name",
                Description = "description",
                CategoryName = "category-name",
                SubCategoryName = "sub-category-name",
                ProductNumber = "product-number",
                Color = "color",
                Size = "size",
                Cost = decimal.MaxValue,
                ListPrice = decimal.MaxValue,
                Quantity = int.MaxValue,
                Clearance = true
            }
        ];

        Product expected = new(
            Id: "00000000-0000-0000-0000-000000000000",
            Name: "name",
            Description: "description",
            Category: "category-name",
            SubCategory: "sub-category-name",
            SKU: "product-number",
            Tags: ["category-name", "sub-category-name", "color", "size"],
            Cost: decimal.MaxValue,
            Price: decimal.MaxValue,
            Quantity: int.MaxValue,
            Clearance: true
        );

        // Act
        Product actual = input.ToProducts().Single();

        // Assert
        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public void ThingToProductTagsMinimumTest()
    {
        // Arrange
        List<Thing> input = [
            new()
            {
                Id = string.Empty,
                Name = string.Empty,
                Description = string.Empty,
                CategoryName = "category-tag",
                SubCategoryName = "sub-category-tag",
                ProductNumber = string.Empty,
                Color = null,
                Size = null,
                Cost = default,
                ListPrice = default,
                Quantity = default,
                Clearance = default
            }
        ];

        string[] expected = ["category-tag", "sub-category-tag"];

        // Act
        Product actual = input.ToProducts().Single();

        // Assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Tags);
        Assert.NotEmpty(actual.Tags);
        Assert.Equal(expected.Length, actual.Tags.Count);
        Assert.Equivalent(expected, actual.Tags);
    }

    [Fact]
    public void ThingToProductTagsColorTagTest()
    {
        // Arrange
        List<Thing> input = [
            new()
            {
                Id = string.Empty,
                Name = string.Empty,
                Description = string.Empty,
                CategoryName = "category-tag",
                SubCategoryName = "sub-category-tag",
                ProductNumber = string.Empty,
                Color = "color-tag",
                Size = default,
                Cost = default,
                ListPrice = default,
                Quantity = default,
                Clearance = default
            }
        ];

        string[] expected = ["category-tag", "sub-category-tag", "color-tag"];

        // Act
        Product actual = input.ToProducts().Single();

        // Assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Tags);
        Assert.NotEmpty(actual.Tags);
        Assert.Equal(expected.Length, actual.Tags.Count);
        Assert.Equivalent(expected, actual.Tags);
    }

    [Fact]
    public void ThingToProductTagsSizeTagTest()
    {
        // Arrange
        List<Thing> input = [
            new()
            {
                Id = string.Empty,
                Name = string.Empty,
                Description = string.Empty,
                CategoryName = "category-tag",
                SubCategoryName = "sub-category-tag",
                ProductNumber = string.Empty,
                Color = default,
                Size = "size-tag",
                Cost = default,
                ListPrice = default,
                Quantity = default,
                Clearance = default
            }
        ];

        string[] expected = ["category-tag", "sub-category-tag", "size-tag"];

        // Act
        Product actual = input.ToProducts().Single();

        // Assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Tags);
        Assert.NotEmpty(actual.Tags);
        Assert.Equal(expected.Length, actual.Tags.Count);
        Assert.Equivalent(expected, actual.Tags);
    }

    [Fact]
    public void ThingToProductTagsMaximumTest()
    {
        // Arrange
        List<Thing> input = [
            new()
            {
                Id = string.Empty,
                Name = string.Empty,
                Description = string.Empty,
                CategoryName = "category-tag",
                SubCategoryName = "sub-category-tag",
                ProductNumber = string.Empty,
                Color = "color-tag",
                Size = "size-tag",
                Cost = default,
                ListPrice = default,
                Quantity = default,
                Clearance = default
            }
        ];

        string[] expected = ["category-tag", "sub-category-tag", "color-tag", "size-tag"];

        // Act
        Product actual = input.ToProducts().Single();

        // Assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Tags);
        Assert.NotEmpty(actual.Tags);
        Assert.Equal(expected.Length, actual.Tags.Count);
        Assert.Equivalent(expected, actual.Tags);
    }
}