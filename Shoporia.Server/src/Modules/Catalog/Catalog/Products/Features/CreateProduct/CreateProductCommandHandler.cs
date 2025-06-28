namespace Catalog.Products.Features.CreateProduct;

public record CreateProductCommand(ProductDTO Product) : ICommand<CreateProductCommandResult>;

public record CreateProductCommandResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
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