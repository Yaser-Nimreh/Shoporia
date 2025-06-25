namespace Catalog.Categories.Features.GetCategories;

public record GetCategoriesResponse(IEnumerable<CategoryDTO> Categories);

public class GetCategoriesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/categories", async (ISender sender) =>
        {
            var query = new GetCategoriesQuery();

            var result = await sender.Send(query);

            var response = result.Adapt<GetCategoriesResponse>();

            return Results.Ok(response);
        })
        .WithName("GetCategories")
        .Produces<GetCategoriesResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get all categories")
        .WithDescription("Retrieves a list of all categories in the catalog.");
    }
}