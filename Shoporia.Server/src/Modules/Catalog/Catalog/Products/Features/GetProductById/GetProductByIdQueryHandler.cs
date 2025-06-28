using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Catalog.Products.Features.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdQueryResult>;

public record GetProductByIdQueryResult(ProductDTO Product);

public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(q => q.Id)
            .NotEmpty().WithMessage("Product ID must not be empty.");
    }
}

public class GetProductByIdQueryHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductByIdQuery, GetProductByIdQueryResult>
{
    public async Task<GetProductByIdQueryResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.AsNoTracking().SingleOrDefaultAsync(p => p.Id == query.Id, cancellationToken) 
            ?? throw new ProductNotFoundException(query.Id);

        var productDTO = product.Adapt<ProductDTO>();

        return new GetProductByIdQueryResult(productDTO);
    }
}