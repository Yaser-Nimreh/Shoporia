namespace Catalog.Categories.Exceptions;

public class CategoryNotFoundException(Guid id) : NotFoundException("Category", id);