namespace Catalog.Categories.Features.GetCategoryById;

public record GetCategoryByIdQuery(Guid Id) : IQuery<GetCategoryByIdQueryResult>;

public record GetCategoryByIdQueryResult(CategoryDTO Category);

public class GetCategoryByIdQueryHandler(CatalogDbContext dbContext) : IQueryHandler<GetCategoryByIdQuery, GetCategoryByIdQueryResult>
{
    public async Task<GetCategoryByIdQueryResult> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
    {
        var category = await dbContext.Categories.AsNoTracking().SingleOrDefaultAsync(c => c.Id == query.Id, cancellationToken) 
            ?? throw new Exception($"Category with ID {query.Id} not found.");

        var categoryDTO = category.Adapt<CategoryDTO>();

        return new GetCategoryByIdQueryResult(categoryDTO);
    }
}