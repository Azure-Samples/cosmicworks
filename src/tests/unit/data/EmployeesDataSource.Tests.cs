// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Data.Tests.Unit;

public class EmployeesDataSourceTests
{
    [Fact]
    public void ZeroCountTest()
    {
        // Arrange
        EmployeesDataSource source = new();

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
        EmployeesDataSource source = new();

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            source.GetItems(count: 235);
        });
    }
}
