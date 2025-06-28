namespace Catalog.Products.Features.GetProducts;

public record GetProductsQuery(PaginationRequest PaginationRequest) : IQuery<GetProductsQueryResult>;

public record GetProductsQueryResult(PaginatedResult<ProductDTO> Products);

public class GetProductsQueryHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductsQuery, GetProductsQueryResult>
{
    public async Task<GetProductsQueryResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.Products.LongCountAsync(cancellationToken);

        var products = await dbContext.Products
            .AsNoTracking()
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

        return new GetProductsQueryResult(paginatedResult);
    }
}