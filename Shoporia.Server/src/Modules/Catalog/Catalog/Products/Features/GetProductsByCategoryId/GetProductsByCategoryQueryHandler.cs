namespace Catalog.Products.Features.GetProductsByCategoryId;

public record GetProductsByCategoryIdQuery(Guid CategoryId, PaginationRequest PaginationRequest) : IQuery<GetProductsByCategoryIdQueryResult>;

public record GetProductsByCategoryIdQueryResult(PaginatedResult<ProductDTO> Products);

public class GetProductsByCategoryIdQueryValidator : AbstractValidator<GetProductsByCategoryIdQuery>
{
    public GetProductsByCategoryIdQueryValidator()
    {
        RuleFor(q => q.CategoryId)
            .NotEmpty().WithMessage("Category ID must not be empty.");
    }
}

public class GetProductsByCategoryQueryHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductsByCategoryIdQuery, GetProductsByCategoryIdQueryResult>
{
    public async Task<GetProductsByCategoryIdQueryResult> Handle(GetProductsByCategoryIdQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.Products
            .Where(p => p.CategoryId == query.CategoryId)
            .LongCountAsync(cancellationToken);

        var products = await dbContext.Products
            .AsNoTracking()
            .Where(p => p.CategoryId == query.CategoryId)
            .OrderBy(p => p.Name)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var productDTOs = products.Adapt<List<ProductDTO>>();

        var paginatedResult = new PaginatedResult<ProductDTO>(
            pageIndex,
            pageSize,
            totalCount,
            productDTOs
        );

        return new GetProductsByCategoryIdQueryResult(paginatedResult);
    }
}