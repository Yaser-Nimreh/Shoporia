namespace Catalog.Products.Features.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdQueryResult>;

public record GetProductByIdQueryResult(ProductDTO Product);

public class GetProductByIdQueryHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductByIdQuery, GetProductByIdQueryResult>
{
    public async Task<GetProductByIdQueryResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.AsNoTracking().SingleOrDefaultAsync(p => p.Id == query.Id, cancellationToken) 
            ?? throw new Exception($"Product with ID {query.Id} not found.");
        
        var productDTO = product.Adapt<ProductDTO>();

        return new GetProductByIdQueryResult(productDTO);
    }
}