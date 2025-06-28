namespace Catalog.Products.Features.UpdateProduct;

public record UpdateProductCommand(ProductDTO Product) : ICommand<UpdateProductCommandResult>;

public record UpdateProductCommandResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(c => c.Product.Id)
            .NotEmpty().WithMessage("Product ID is required.");

        RuleFor(c => c.Product.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");
        
        RuleFor(c => c.Product.Description)
            .NotEmpty().WithMessage("Product description is required.")
            .MaximumLength(500).WithMessage("Product description must not exceed 500 characters.");
        
        RuleFor(c => c.Product.Price)
            .GreaterThan(0).WithMessage("Product price must be greater than zero.");
        
        RuleFor(c => c.Product.CategoryId)
            .NotEmpty().WithMessage("Category ID is required.");
        
        RuleFor(c => c.Product.ImageFilePath)
            .NotEmpty().WithMessage("Image file path is required.")
            .MaximumLength(200).WithMessage("Image file path must not exceed 200 characters.");
    }
}

public class UpdateProductCommandHandler(CatalogDbContext dbContext) : ICommandHandler<UpdateProductCommand, UpdateProductCommandResult>
{
    public async Task<UpdateProductCommandResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.FindAsync([command.Product.Id], cancellationToken) 
            ?? throw new ProductNotFoundException(command.Product.Id);
        
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