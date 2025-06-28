namespace Catalog.Products.Features.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductCommandResult>;

public record DeleteProductCommandResult(bool IsSuccess);

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Product ID is required.");
    }
}

public class DeleteProductCommandHandler(CatalogDbContext dbContext) : ICommandHandler<DeleteProductCommand, DeleteProductCommandResult>
{
    public async Task<DeleteProductCommandResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.FindAsync([command.Id], cancellationToken) 
            ?? throw new ProductNotFoundException(command.Id);

        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteProductCommandResult(true);
    }
}