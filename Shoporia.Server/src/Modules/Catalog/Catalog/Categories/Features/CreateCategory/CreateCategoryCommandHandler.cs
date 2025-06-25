namespace Catalog.Categories.Features.CreateCategory;

public record CreateCategoryCommand(CategoryDTO Category) : ICommand<CreateCategoryCommandResult>;

public record CreateCategoryCommandResult(Guid Id);

public class CreateCategoryCommandHandler(CatalogDbContext dbContext) : ICommandHandler<CreateCategoryCommand, CreateCategoryCommandResult>
{
    public async Task<CreateCategoryCommandResult> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = CreateNewCategory(command.Category);

        await dbContext.Categories.AddAsync(category, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateCategoryCommandResult(category.Id);
    }

    private static Category CreateNewCategory(CategoryDTO categoryDTO)
    {
        var category = Category.Create(
            Guid.NewGuid(),
            categoryDTO.Name,
            categoryDTO.Description);

        return category;
    }
}