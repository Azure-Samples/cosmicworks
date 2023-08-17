namespace CosmicWorks.Data.Models.Raw;

internal sealed record Thing(
    string Name,
    string Description,
    string CategoryName,
    string SubCategoryName,
    string ProductNumber,
    string Color,
    string Size,
    decimal ListPrice
);