namespace CosmicWorks.Tool.Models;

public record Product(
    string Id,
    string TenantId,
    string TenantName,
    string VendorId,
    string VendorName,
    string CategoryId,
    string CategoryName,
    string SKU,
    string Name,
    string Description,
    decimal Price
)
{
    public IEnumerable<Tag> Tags { get; init; } = Enumerable.Empty<Tag>();
}