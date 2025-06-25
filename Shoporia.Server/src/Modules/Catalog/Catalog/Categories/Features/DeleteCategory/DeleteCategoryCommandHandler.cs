namespace Catalog.Categories.Features.DeleteCategory;

public record DeleteCategoryCommand(Guid Id) : ICommand<DeleteCategoryCommandResult>;

public record DeleteCategoryCommandResult(bool IsSuccess);

public class DeleteCategoryCommandHandler(CatalogDbContext dbContext) : ICommandHandler<DeleteCategoryCommand, DeleteCategoryCommandResult>
{
    public async Task<DeleteCategoryCommandResult> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await dbContext.Categories.FindAsync([command.Id], cancellationToken) 
            ?? throw new Exception($"Category with ID {command.Id} not found.");
        
        dbContext.Categories.Remove(category);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteCategoryCommandResult(true);
    }
}