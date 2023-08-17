using Bogus;
using CosmicWorks.Tool.Interfaces;
using CosmicWorks.Tool.Models;
using Slugify;

namespace CosmicWorks.Tool.Services;

public class BogusProductGeneratorService : IGeneratorService<Product>
{
    public async Task<IReadOnlyCollection<Product>> GenerateDataAsync(int count)
    {
        return await Task<IReadOnlyCollection<Product>>.Factory.StartNew(() =>
        {
            SlugHelper slugHelper = new();
            return new Faker<Product>()
                .CustomInstantiator(f =>
                    {
                        string tenant = "Adventure Works";
                        string vendor = f.Company.CompanyName();
                        string category = f.Commerce.Department();
                        return new Product(
                            Id: $"{f.Random.Guid()}",
                            TenantId: slugHelper.GenerateSlug(tenant),
                            TenantName: tenant,
                            VendorId: slugHelper.GenerateSlug(vendor),
                            VendorName: vendor,
                            CategoryId: slugHelper.GenerateSlug(category),
                            CategoryName: category,
                            SKU: f.Commerce.Ean8(),
                            Name: f.Commerce.ProductName(),
                            Description: f.Commerce.ProductDescription(),
                            Price: f.Finance.Amount()
                        );
                    }
                )
                .Generate(count)
                .AsReadOnly();
        });
    }
}
