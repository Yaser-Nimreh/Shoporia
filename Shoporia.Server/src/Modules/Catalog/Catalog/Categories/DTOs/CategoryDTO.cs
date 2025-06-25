namespace Catalog.Categories.DTOs;

public record CategoryDTO(
    Guid Id,
    string Name,
    string Description
);