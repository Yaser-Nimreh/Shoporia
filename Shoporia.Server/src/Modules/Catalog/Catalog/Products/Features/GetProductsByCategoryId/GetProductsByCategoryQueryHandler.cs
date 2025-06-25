namespace Catalog.Products.Features.GetProductsByCategoryId;

public record GetProductsByCategoryIdQuery(Guid CategoryId) : IQuery<GetProductsByCategoryIdQueryResult>;

public record GetProductsByCategoryIdQueryResult(IEnumerable<ProductDTO> Products);

public class GetProductsByCategoryQueryHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductsByCategoryIdQuery, GetProductsByCategoryIdQueryResult>
{
    public async Task<GetProductsByCategoryIdQueryResult> Handle(GetProductsByCategoryIdQuery query, CancellationToken cancellationToken)
    {
        var products = await dbContext.Products
            .AsNoTracking()
            .Where(p => p.CategoryId == query.CategoryId)
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);

        var productDTOs = products.Adapt<List<ProductDTO>>();

        return new GetProductsByCategoryIdQueryResult(productDTOs);
    }
}