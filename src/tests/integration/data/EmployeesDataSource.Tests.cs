// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Data.Tests.Integration;

public class EmployeesDataSourceTests
{
    [Fact]
    public void GetAllEmployeesTest()
    {
        // Arrange
        EmployeesDataSource source = new();

        // Act
        IReadOnlyList<Employee> result = source.GetItems();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.All(result, Assert.NotNull);
        Assert.Equal(EmployeesDataSource.MaxEmployeesCount, result.Count);
    }

    [Fact]
    public void GetSomeEmployeesTest()
    {
        // Arrange
        EmployeesDataSource source = new();

        // Act
        IReadOnlyList<Employee> result = source.GetItems(count: 100);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.All(result, Assert.NotNull);
        Assert.Equal(100, result.Count);
    }

    [Fact]
    public void GetValidEmployeeTest()
    {
        // Arrange
        EmployeesDataSource source = new();

        // Act
        Employee result = source.GetItems(count: 1).Single();

        // Assert
        Assert.NotNull(result);
        Assert.False(string.IsNullOrEmpty(result.Id));
        Assert.False(string.IsNullOrEmpty(result.Company));
        Assert.False(string.IsNullOrEmpty(result.Department));
        Assert.False(string.IsNullOrEmpty(result.EmailAddress));
        Assert.False(string.IsNullOrEmpty(result.PhoneNumber));
        Assert.False(string.IsNullOrEmpty(result.Territory));
        Assert.NotNull(result.Name);
        Assert.False(string.IsNullOrEmpty(result.Name.First));
        Assert.False(string.IsNullOrEmpty(result.Name.Last));
        Assert.NotEmpty(result.Addresses);
        Assert.All(result.Addresses, Assert.NotNull);
        Assert.All(result.Addresses, address =>
        {
            Assert.False(string.IsNullOrEmpty(address.Name));
            Assert.False(string.IsNullOrEmpty(address.Street));
            Assert.False(string.IsNullOrEmpty(address.City));
            Assert.False(string.IsNullOrEmpty(address.State));
            Assert.False(string.IsNullOrEmpty(address.ZipCode));
        });
    }
}