// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Tool.Tests.Unit;

public class GenerateDataSettingsTests
{
    [Fact]
    public void NoArgumentsValidationError()
    {
        // Arrange
        GenerateDataSettings settings = new();

        // Act
        ValidationResult result = settings.Validate();

        // Assert
        Assert.False(result.Successful);
        Assert.Equal("You must provide a connection string, and endpoint (for RBAC), or use the emulator.", result.Message);
    }

    [Fact]
    public void OnlyEndpointValidationError()
    {
        // Arrange
        GenerateDataSettings settings = new()
        {
            Endpoint = "<endpoint>",
        };

        // Act
        ValidationResult result = settings.Validate();

        // Assert
        Assert.False(result.Successful);
        Assert.Equal("You can't provide an endpoint without using role-based access control.", result.Message);
    }

    [Fact]
    public void OnlyRBACValidationError()
    {
        // Arrange
        GenerateDataSettings settings = new()
        {
            RoleBasedAccessControl = true,
        };

        // Act
        ValidationResult result = settings.Validate();

        // Assert
        Assert.False(result.Successful);
        Assert.Equal("You must provide an endpoint when using role-based access control.", result.Message);
    }

    [Fact]
    public void ConnectionStringAndRBACValidationError()
    {
        // Arrange
        GenerateDataSettings settings = new()
        {
            ConnectionString = "<connection-string>",
            RoleBasedAccessControl = true,
        };

        // Act
        ValidationResult result = settings.Validate();

        // Assert
        Assert.False(result.Successful);
        Assert.Equal("You can't provide a connection string when using role-based access control.", result.Message);
    }

    [Fact]
    public void EmulatorAndRBACValidationError()
    {
        // Arrange
        GenerateDataSettings settings = new()
        {
            RoleBasedAccessControl = true,
            Emulator = true,
        };

        // Act
        ValidationResult result = settings.Validate();

        // Assert
        Assert.False(result.Successful);
        Assert.Equal("You can't use the emulator when using role-based access control.", result.Message);
    }

    [Fact]
    public void ConnectionStringAndEndpointValidationError()
    {
        // Arrange
        GenerateDataSettings settings = new()
        {
            ConnectionString = "<connection-string>",
            Endpoint = "<endpoint>",
        };

        // Act
        ValidationResult result = settings.Validate();

        // Assert
        Assert.False(result.Successful);
        Assert.Equal("You can't provide both an endpoint and a connection string.", result.Message);
    }

    [Fact]
    public void ConnectionStringAndEmulatorValidationError()
    {
        // Arrange
        GenerateDataSettings settings = new()
        {
            ConnectionString = "<connection-string>",
            Emulator = true,
        };

        // Act
        ValidationResult result = settings.Validate();

        // Assert
        Assert.False(result.Successful);
        Assert.Equal("You can't provide a connection string when using the emulator.", result.Message);
    }

    [Fact]
    public void EndpointAndEmulatorValidationError()
    {
        // Arrange
        GenerateDataSettings settings = new()
        {
            Endpoint = "<endpoint>",
            Emulator = true,
        };

        // Act
        ValidationResult result = settings.Validate();

        // Assert
        Assert.False(result.Successful);
        Assert.Equal("You can't provide an endpoint when using the emulator.", result.Message);
    }

    [Fact]
    public void EndpointAndRBACValidationSuccess()
    {
        // Arrange
        GenerateDataSettings settings = new()
        {
            Endpoint = "<endpoint>",
            RoleBasedAccessControl = true,
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
        GenerateDataSettings settings = new()
        {
            Emulator = true,
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
        GenerateDataSettings settings = new()
        {
            ConnectionString = "<connection-string>",
        };

        // Act
        ValidationResult result = settings.Validate();

        // Assert
        Assert.True(result.Successful);
    }
}