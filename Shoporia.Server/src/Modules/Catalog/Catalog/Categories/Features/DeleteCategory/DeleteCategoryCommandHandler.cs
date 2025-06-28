namespace Catalog.Categories.Features.DeleteCategory;

public record DeleteCategoryCommand(Guid Id) : ICommand<DeleteCategoryCommandResult>;

public record DeleteCategoryCommandResult(bool IsSuccess);

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Category ID is required.");
    }
}

public class DeleteCategoryCommandHandler(CatalogDbContext dbContext) : ICommandHandler<DeleteCategoryCommand, DeleteCategoryCommandResult>
{
    public async Task<DeleteCategoryCommandResult> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await dbContext.Categories.FindAsync([command.Id], cancellationToken) 
            ?? throw new CategoryNotFoundException(command.Id);
        
        dbContext.Categories.Remove(category);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteCategoryCommandResult(true);
    }
}