namespace Catalog.Products.Features.GetProducts;

public record GetProductsQuery : IQuery<GetProductsQueryResult>;

public record GetProductsQueryResult(IEnumerable<ProductDTO> Products);

public class GetProductsQueryHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductsQuery, GetProductsQueryResult>
{
    public async Task<GetProductsQueryResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await dbContext.Products
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);

        var productDTOs = products.Adapt<List<ProductDTO>>();

        return new GetProductsQueryResult(productDTOs);
    }
}