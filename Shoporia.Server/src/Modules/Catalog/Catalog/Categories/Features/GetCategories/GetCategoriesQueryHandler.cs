namespace Catalog.Categories.Features.GetCategories;

public record GetCategoriesQuery : IQuery<GetCategoriesQueryResult>;

public record GetCategoriesQueryResult(IEnumerable<CategoryDTO> Categories);

public class GetCategoriesQueryHandler(CatalogDbContext dbContext) : IQueryHandler<GetCategoriesQuery, GetCategoriesQueryResult>
{
    public async Task<GetCategoriesQueryResult> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
    {
        var categories = await dbContext.Categories
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken);

        var categoryDTOs = categories.Adapt<List<CategoryDTO>>();

        return new GetCategoriesQueryResult(categoryDTOs);
    }
}