using SharedKernel.Pagination;

namespace Catalog.Products.Features.GetProducts;

public record GetProductsResponse(PaginatedResult<ProductDTO> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] PaginationRequest paginationRequest, ISender sender) =>
        {
            var query = new GetProductsQuery(paginationRequest);

            var result = await sender.Send(query);

            var response = result.Adapt<GetProductsResponse>();

            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get all products")
        .WithDescription("Retrieves a list of all products in the catalog.");
    }
}