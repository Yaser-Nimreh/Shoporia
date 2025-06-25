namespace Catalog.Categories.Features.GetCategoryById;

public record GetCategoryByIdResponse(CategoryDTO Category);

public class GetCategoryByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/categories/{id:guid}", async (Guid id, ISender sender) =>
        {
            var query = new GetCategoryByIdQuery(id);

            var result = await sender.Send(query);

            var response = result.Adapt<GetCategoryByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetCategoryById")
        .Produces<GetCategoryByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get category by ID")
        .WithDescription("Retrieves a category by its unique identifier.");
    }
}