// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Models;

/// <summary>
/// Represents an employee.
/// </summary>
/// <param name="Id">
/// The unique identifier of the employee.
/// </param>
/// <param name="Name">
/// The name of the employee.
/// </param>
/// <param name="Addresses">
/// The set of addresses associated with the employee.
/// </param>
/// <param name="Company">
/// The company associated with the employee.
/// </param>
/// <param name="Department">
/// The department associated with the employee.
/// </param>
/// <param name="EmailAddress">
/// The email address of the employee.
/// </param>
/// <param name="PhoneNumber">
/// The phone number of the employee.
/// </param>
/// <param name="Territory">
/// The territory associated with the employee.
/// </param>
/// <param name="Type">
/// The type of the item. Default is "Employee".
/// </param>
public sealed record Employee(
    string Id,
    Name Name,
    IList<Address> Addresses,
    string Company,
    string Department,
    string EmailAddress,
    string PhoneNumber,
    string Territory,
    string Type = nameof(Employee)
)
{
    /// <summary>
    /// Outputs the product as a string.
    /// </summary>
    /// <returns>
    /// A string containing the product's unique identifier, name, and category.
    /// </returns>
    public override string ToString() => $"{Id} | {Name.First} {Name.Last}";
}