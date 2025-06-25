namespace Catalog.Products.Features.CreateProduct;

public record CreateProductCommand(ProductDTO Product) : ICommand<CreateProductCommandResult>;

public record CreateProductCommandResult(Guid Id);

public class CreateProductCommandHandler(CatalogDbContext dbContext) : ICommandHandler<CreateProductCommand, CreateProductCommandResult>
{
    public async Task<CreateProductCommandResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = CreateNewProduct(command.Product);

        await dbContext.Products.AddAsync(product, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateProductCommandResult(product.Id);
    }

    private static Product CreateNewProduct(ProductDTO productDTO)
    {
        var product = Product.Create(
            Guid.NewGuid(),
            productDTO.Name,
            productDTO.Description,
            productDTO.Price,
            productDTO.CategoryId,
            productDTO.ImageFilePath);

        return product;
    }
}