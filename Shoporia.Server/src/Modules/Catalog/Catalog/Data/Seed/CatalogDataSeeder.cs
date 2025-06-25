namespace Catalog.Data.Seed;

public class CatalogDataSeeder(CatalogDbContext dbContext) : IDataSeeder
{
    public async Task SeedAllAsync()
    {
        var categoriesExist = await dbContext.Categories.AnyAsync();

        if (!categoriesExist)
        {
            await dbContext.Categories.AddRangeAsync(InitialData.Categories);
            await dbContext.SaveChangesAsync();
        }

        var categoriesNowExist = await dbContext.Categories.AnyAsync();
        var productsExist = await dbContext.Products.AnyAsync();

        // Only seed products if categories are present (whether from before or just seeded)
        if (categoriesNowExist && !productsExist)
        {
            await dbContext.Products.AddRangeAsync(InitialData.Products);
            await dbContext.SaveChangesAsync();
        }
    }
}