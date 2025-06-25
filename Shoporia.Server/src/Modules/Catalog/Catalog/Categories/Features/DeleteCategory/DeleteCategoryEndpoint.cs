namespace Catalog.Categories.Features.DeleteCategory;

public record DeleteCategoryResponse(bool IsSuccess);

public class DeleteCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/categories/{id:guid}", async (Guid id, ISender sender) =>
        {
            var command = new DeleteCategoryCommand(id);

            var result = await sender.Send(command);

            var response = result.Adapt<DeleteCategoryResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteCategory")
        .Produces<DeleteCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Deletes a category from the catalog")
        .WithDescription("Deletes the specified category from the catalog database using its unique identifier. If the category does not exist, a 404 Not Found response is returned.");
    }
}