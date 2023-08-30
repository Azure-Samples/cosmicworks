namespace CosmicWorks.Data.Models;

public sealed record Product(
    string Id,
    string Name,
    string Description,
    Category Category,
    string SKU,
    IList<string> Tags,
    decimal Price,
    string Type = nameof(Product)
)
{
    public override string ToString() => $"{Id} | {Name} - {Category.Name}";
}