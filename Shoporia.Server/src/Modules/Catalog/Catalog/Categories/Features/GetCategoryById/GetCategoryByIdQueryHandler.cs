using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Catalog.Categories.Features.GetCategoryById;

public record GetCategoryByIdQuery(Guid Id) : IQuery<GetCategoryByIdQueryResult>;

public record GetCategoryByIdQueryResult(CategoryDTO Category);

public class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
{
    public GetCategoryByIdQueryValidator()
    {
        RuleFor(q => q.Id)
            .NotEmpty().WithMessage("Category ID is required.");
    }
}

public class GetCategoryByIdQueryHandler(CatalogDbContext dbContext) : IQueryHandler<GetCategoryByIdQuery, GetCategoryByIdQueryResult>
{
    public async Task<GetCategoryByIdQueryResult> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
    {
        var category = await dbContext.Categories.AsNoTracking().SingleOrDefaultAsync(c => c.Id == query.Id, cancellationToken) 
            ?? throw new CategoryNotFoundException(query.Id);

        var categoryDTO = category.Adapt<CategoryDTO>();

        return new GetCategoryByIdQueryResult(categoryDTO);
    }
}