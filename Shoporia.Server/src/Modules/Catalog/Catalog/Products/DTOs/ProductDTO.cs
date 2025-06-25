namespace Catalog.Products.DTOs;

public record ProductDTO(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    Guid CategoryId,
    string ImageFilePath
);