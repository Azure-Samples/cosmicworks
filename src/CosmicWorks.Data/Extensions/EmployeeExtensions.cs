using Bogus;
using CosmicWorks.Data.Models;
using Raw = CosmicWorks.Data.Models.Raw;

namespace CosmicWorks.Data.Extensions;

internal static class EmployeeExtensions
{
    private static readonly Faker faker = new("en");

    public static IEnumerable<Employee> ToEmployees(this IEnumerable<Raw.Person> items) =>
        items.Select(i => new Employee(
                Name: new Name(
                    First: i.First,
                    Last: i.Last
                ),
                Addresses: new List<Address> {
                    new Address(
                        Name: "Headquarters",
                        Street: $"1234 Oak Street",
                        City: "Redmond",
                        State: "WA",
                        ZipCode: "23052"
                    )
                },
                Company: "Adventure Works",
                Department: faker.Random.ArrayElement<string>("HR", "IT", "Marketing", "Sales"),
                EmailAddress: $"{i.First.ToLowerInvariant()}@adventure-works.com",
                PhoneNumber: $"425-555-{faker.Random.Int(min: 100, max: 199):0000}",
                Territory: i.Region,
                Type: "employee"
            )
        );
}