// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Data.Tests.Unit;

public class EmployeeExtensionsTests
{
    [Fact]
    public void NullInputTest()
    {
        // Arrange
        IEnumerable<Person> input = default!;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            input.ToEmployees()
        );
    }

    [Fact]
    public void NullItemInputTest()
    {
        // Arrange
        List<Person> input = [
            default!
        ];

        // Act & Assert
        Assert.Throws<NullReferenceException>(() =>
        {
            IEnumerable<Employee> actual = input.ToEmployees();
            actual.Count();
        });
    }

    [Fact]
    public void EmptyInputTest()
    {
        // Arrange
        List<Person> input = [];

        // Act
        IEnumerable<Employee> actual = input.ToEmployees();

        // Assert
        Assert.Empty(actual);
    }

    [Fact]
    public void InputSameCountTest()
    {
        // Arrange
        List<Person> input = [
            new Person(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)
        ];

        // Act
        IEnumerable<Employee> actual = input.ToEmployees();

        // Assert
        Assert.Equal(input.Count, actual.Count());
    }

    [Fact]
    public void PersonToEmployeeTest()
    {
        // Arrange
        List<Person> input = [
            new()
            {
                Id = "00000000-0000-0000-0000-000000000000",
                First = "first-name",
                Last = "last-name",
                Department = "department",
                PhoneNumberSuffix = "0000",
                Region = "region"
            }
        ];

        Employee expected = new(
            Id: "00000000-0000-0000-0000-000000000000",
            Name: new Name(
                First: "first-name",
                Last: "last-name"
            ),
            Addresses:
            [
                new Address(
                    Name: "Headquarters",
                    Street: "1234 Oak Street",
                    City: "Redmond",
                    State: "WA",
                    ZipCode: "23052"
                )
            ],
            Company: "Adventure Works",
            Department: "department",
            EmailAddress: "first-name@adventure-works.com",
            PhoneNumber: "425-555-0000",
            Territory: "region"
        );

        // Act
        Employee actual = input.ToEmployees().Single();

        // Assert
        Assert.Equivalent(expected, actual);
    }
}