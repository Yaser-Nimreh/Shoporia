namespace Catalog.Categories.Events;

public record CategoryUpdatedEvent(Category Category) : IDomainEvent;