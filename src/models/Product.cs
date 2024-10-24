namespace CosmicWorks.Models;

public sealed record Product(
    string Id,
    string Name,
    string Description,
    Category Category,
    string SKU,
    IList<string> Tags,
    decimal Cost,
    decimal Price,
    string Type = nameof(Product)
)
{
    public override string ToString() => $"{Id} | {Name} - {Category.Name}";
}