// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Data.Extensions;

internal static class EmployeeExtensions
{
    public static IEnumerable<Employee> ToEmployees(this IEnumerable<Person> items) =>
        items.Select(i => new Employee(
                Id: i.Id,
                Name: new Name(
                    First: i.First,
                    Last: i.Last
                ),
                Addresses: [
                    new Address(
                        Name: "Headquarters",
                        Street: $"1234 Oak Street",
                        City: "Redmond",
                        State: "WA",
                        ZipCode: "23052"
                    )
                ],
                Company: "Adventure Works",
                Department: i.Department,
                EmailAddress: $"{i.First.ToLowerInvariant()}@adventure-works.com",
                PhoneNumber: $"425-555-{i.PhoneNumberSuffix}",
                Territory: i.Region,
                Type: "employee"
            )
        );
}