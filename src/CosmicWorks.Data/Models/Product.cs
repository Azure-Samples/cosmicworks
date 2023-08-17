namespace CosmicWorks.Data.Models;

public sealed record Product(
    string Name,
    string Description,
    Category Category,
    string SKU,
    IList<string> Tags,
    decimal Price
)
{
    public string Id { get; init; } = $"{Guid.NewGuid()}";

    public override string ToString() => $"{Id} | {Name} - {Category.Name}";
}