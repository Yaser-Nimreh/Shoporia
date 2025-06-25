namespace Catalog.Products.Features.UpdateProduct;

public record UpdateProductCommand(ProductDTO Product) : ICommand<UpdateProductCommandResult>;

public record UpdateProductCommandResult(bool IsSuccess);

public class UpdateProductCommandHandler(CatalogDbContext dbContext) : ICommandHandler<UpdateProductCommand, UpdateProductCommandResult>
{
    public async Task<UpdateProductCommandResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.FindAsync([command.Product.Id], cancellationToken) 
            ?? throw new Exception($"Product with ID {command.Product.Id} not found.");
        
        UpdateProductWithNewValues(product, command.Product);

        dbContext.Products.Update(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateProductCommandResult(true);
    }

    private static void UpdateProductWithNewValues(Product product, ProductDTO productDTO)
    {
        product.Update(
            productDTO.Name,
            productDTO.Description,
            productDTO.Price,
            productDTO.CategoryId,
            productDTO.ImageFilePath);
    }
}