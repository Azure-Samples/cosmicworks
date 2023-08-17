namespace CosmicWorks.Data.Models;

public sealed record Employee(
    Name Name,
    IList<Address> Addresses,
    string Company,
    string Department,
    string EmailAddress,
    string PhoneNumber,
    string Territory,
    string Type = "employee"
)
{
    public string Id { get; init; } = $"{Guid.NewGuid()}";

    public override string ToString() => $"{Id} | {Name.First} {Name.Last}";
}