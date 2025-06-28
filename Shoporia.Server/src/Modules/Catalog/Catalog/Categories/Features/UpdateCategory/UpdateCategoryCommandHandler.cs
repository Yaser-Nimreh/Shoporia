namespace Catalog.Categories.Features.UpdateCategory;

public record UpdateCategoryCommand(CategoryDTO Category) : ICommand<UpdateCategoryCommandResult>;

public record UpdateCategoryCommandResult(bool IsSuccess);

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(c => c.Category.Id)
            .NotEmpty().WithMessage("Category ID is required.");

        RuleFor(c => c.Category.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");
        
        RuleFor(c => c.Category.Description)
            .NotEmpty().WithMessage("Category description is required.")
            .MaximumLength(500).WithMessage("Category description must not exceed 500 characters.");
    }
}

public class UpdateCategoryCommandHandler(CatalogDbContext dbContext) : ICommandHandler<UpdateCategoryCommand, UpdateCategoryCommandResult>
{
    public async Task<UpdateCategoryCommandResult> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await dbContext.Categories.FindAsync([command.Category.Id], cancellationToken) 
            ?? throw new CategoryNotFoundException(command.Category.Id);

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