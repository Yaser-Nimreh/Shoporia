namespace Catalog.Categories.Features.GetCategories;

public record GetCategoriesQuery(PaginationRequest PaginationRequest) : IQuery<GetCategoriesQueryResult>;

public record GetCategoriesQueryResult(PaginatedResult<CategoryDTO> Categories);

public class GetCategoriesQueryHandler(CatalogDbContext dbContext) : IQueryHandler<GetCategoriesQuery, GetCategoriesQueryResult>
{
    public async Task<GetCategoriesQueryResult> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.Categories.LongCountAsync(cancellationToken);

        var categories = await dbContext.Categories
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var categoryDTOs = categories.Adapt<List<CategoryDTO>>();

        var paginatedResult = new PaginatedResult<CategoryDTO>(
            pageIndex,
            pageSize,
            totalCount,
            categoryDTOs
        );

        return new GetCategoriesQueryResult(paginatedResult);
    }
}