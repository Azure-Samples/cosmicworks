// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Tool.Tests.Unit;

public class GenerateProductsSettingsTests
{
    [Fact]
    public void NoArgumentsValidationError()
    {
        // Arrange
        GenerateProductsSettings settings = new();

        // Act
        ValidationResult result = settings.Validate();

        // Assert
        Assert.False(result.Successful);
        Assert.Equal("You must provide a connection string, an endpoint, or use the emulator.", result.Message);
    }

    [Fact]
    public void EmulatorAndEndpointValidationError()
    {
        // Arrange
        GenerateProductsSettings settings = new()
        {
            Emulator = true,
            Endpoint = "<endpoint>"
        };

        // Act
        ValidationResult result = settings.Validate();

        // Assert
        Assert.False(result.Successful);
        Assert.Equal("You can't provide an endpoint when using the emulator.", result.Message);
    }

    [Fact]
    public void EmulatorAndConnectionStringValidationError()
    {
        // Arrange
        GenerateProductsSettings settings = new()
        {
            Emulator = true,
            ConnectionString = "<connection-string>"
        };

        // Act
        ValidationResult result = settings.Validate();

        // Assert
        Assert.False(result.Successful);
        Assert.Equal("You can't provide a connection string when using the emulator.", result.Message);
    }

    [Fact]
    public void ConnectionStringAndEndpointValidationError()
    {
        // Arrange
        GenerateProductsSettings settings = new()
        {
            ConnectionString = "<connection-string>",
            Endpoint = "<endpoint>"
        };

        // Act
        ValidationResult result = settings.Validate();

        // Assert
        Assert.False(result.Successful);
        Assert.Equal("You can't provide both an endpoint and a connection string.", result.Message);
    }

    [Fact]
    public void EndpointValidationSuccess()
    {
        // Arrange
        GenerateProductsSettings settings = new()
        {
            Endpoint = "<endpoint>"
        };

        // Act
        ValidationResult result = settings.Validate();

        // Assert
        Assert.True(result.Successful);
    }

    [Fact]
    public void EmulatorValidationSuccess()
    {
        // Arrange
        GenerateProductsSettings settings = new()
        {
            Emulator = true
        };

        // Act
        ValidationResult result = settings.Validate();

        // Assert
        Assert.True(result.Successful);
    }

    [Fact]
    public void ConnectionStringValidationSuccess()
    {
        // Arrange
        GenerateProductsSettings settings = new()
        {
            ConnectionString = "<connection-string>"
        };

        // Act
        ValidationResult result = settings.Validate();

        // Assert
        Assert.True(result.Successful);
    }

    [Fact]
    public void EmptyEndpointTest()
    {
        // Arrange
        GenerateProductsSettings settings = new()
        {
            Endpoint = string.Empty
        };

        // Act
        ValidationResult result = settings.Validate();

        // Assert
        Assert.False(result.Successful);
        Assert.Equal("You must provide a valid value for the endpoint.", result.Message);
    }

    [Fact]
    public void EmptyConnectionStringTest()
    {
        // Arrange
        GenerateProductsSettings settings = new()
        {
            ConnectionString = string.Empty
        };

        // Act
        ValidationResult result = settings.Validate();

        // Assert
        Assert.False(result.Successful);
        Assert.Equal("You must provide a valid value for the connection string.", result.Message);
    }

    [Fact]
    public void NegativeQuantityTest()
    {
        // Arrange
        GenerateProductsSettings settings = new()
        {
            Emulator = true,
            Quantity = int.MinValue
        };

        // Act
        ValidationResult result = settings.Validate();

        // Assert
        Assert.False(result.Successful);
        Assert.Equal("The quantity must be at least 1.", result.Message);
    }

    [Fact]
    public void ZeroQuantityTest()
    {
        // Arrange
        GenerateProductsSettings settings = new()
        {
            Emulator = true,
            Quantity = 0
        };

        // Act
        ValidationResult result = settings.Validate();

        // Assert
        Assert.False(result.Successful);
        Assert.Equal("The quantity must be at least 1.", result.Message);
    }

    [Fact]
    public void TooLargeQuantityTest()
    {
        // Arrange
        GenerateProductsSettings settings = new()
        {
            Emulator = true,
            Quantity = int.MaxValue
        };

        // Act
        ValidationResult result = settings.Validate();

        // Assert
        Assert.False(result.Successful);
        Assert.Equal("The quantity must be less than 1760.", result.Message);
    }

    [Fact]
    public void MaximumQuantityTest()
    {
        // Arrange
        GenerateProductsSettings settings = new()
        {
            Emulator = true,
            Quantity = 1759
        };

        // Act
        ValidationResult result = settings.Validate();

        // Assert
        Assert.True(result.Successful);
    }
}