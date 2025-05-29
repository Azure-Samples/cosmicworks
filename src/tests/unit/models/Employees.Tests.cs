// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Models.Tests.Unit;

public class EmployeesTests
{
    [Fact]
    public void ToStringTest()
    {
        // Arrange
        Employee target = new(
            Id: "00000000-0000-0000-0000-000000000000",
            Name: new Name(
                First: "first",
                Last: "last"
            ),
            Addresses: [],
            Company: string.Empty,
            Department: string.Empty,
            EmailAddress: string.Empty,
            PhoneNumber: string.Empty,
            Territory: string.Empty
        );

        // Act
        string actual = target.ToString();

        // Assert
        Assert.Equal("00000000-0000-0000-0000-000000000000 | first last", actual);
    }
}