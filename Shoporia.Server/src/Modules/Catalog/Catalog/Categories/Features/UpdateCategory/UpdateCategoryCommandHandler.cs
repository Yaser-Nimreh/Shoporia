namespace Catalog.Categories.Features.UpdateCategory;

public record UpdateCategoryCommand(CategoryDTO Category) : ICommand<UpdateCategoryCommandResult>;

public record UpdateCategoryCommandResult(bool IsSuccess);

public class UpdateCategoryCommandHandler(CatalogDbContext dbContext) : ICommandHandler<UpdateCategoryCommand, UpdateCategoryCommandResult>
{
    public async Task<UpdateCategoryCommandResult> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await dbContext.Categories.FindAsync([command.Category.Id], cancellationToken) 
            ?? throw new Exception($"Category with ID {command.Category.Id} not found.");
        
        UpdateCategoryWithNewValues(category, command.Category);

        dbContext.Categories.Update(category);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateCategoryCommandResult(true);
    }

    private static void UpdateCategoryWithNewValues(Category category, CategoryDTO categoryDTO)
    {
        category.Update(
            categoryDTO.Name,
            categoryDTO.Description);
    }
}