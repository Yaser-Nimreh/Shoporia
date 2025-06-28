namespace Catalog.Categories.Features.CreateCategory;

public record CreateCategoryCommand(CategoryDTO Category) : ICommand<CreateCategoryCommandResult>;

public record CreateCategoryCommandResult(Guid Id);

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(c => c.Category.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");
        
        RuleFor(c => c.Category.Description)
            .NotEmpty().WithMessage("Category description is required.")
            .MaximumLength(500).WithMessage("Category description must not exceed 500 characters.");
    }
}

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