namespace Catalog.Categories.Features.UpdateCategory;

public record UpdateCategoryRequest(CategoryDTO Category);

public record UpdateCategoryResponse(bool IsSuccess);

public class UpdateCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/categories", async (UpdateCategoryRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateCategoryCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<UpdateCategoryResponse>();

            return Results.Ok(response);
        })
        .WithName("UpdateCategory")
        .Produces<UpdateCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Updates an existing category in the catalog")
        .WithDescription("Receives updated category details including name and description, then updates the specified category in the catalog database.");
    }
}