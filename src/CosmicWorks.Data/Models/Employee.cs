namespace CosmicWorks.Data.Models;

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
    public override string ToString() => $"{Id} | {Name.First} {Name.Last}";
}