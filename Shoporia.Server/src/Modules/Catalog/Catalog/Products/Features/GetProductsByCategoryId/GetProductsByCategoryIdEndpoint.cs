using SharedKernel.Pagination;

namespace Catalog.Products.Features.GetProductsByCategoryId;

public record GetProductsByCategoryIdResponse(PaginatedResult<ProductDTO> Products);

public class GetProductsByCategoryIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{categoryId:guid}", async (Guid categoryId, [AsParameters] PaginationRequest paginationRequest, ISender sender) =>
        {
            var query = new GetProductsByCategoryIdQuery(categoryId, paginationRequest);

            var result = await sender.Send(query);

            var response = result.Adapt<GetProductsByCategoryIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetProductsByCategoryId")
        .Produces<GetProductsByCategoryIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products by Category ID")
        .WithDescription("Retrieves a list of products associated with a specific category ID.");
    }
}