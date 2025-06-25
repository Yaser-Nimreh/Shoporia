namespace Catalog.Categories.Events;

public record CategoryCreatedEvent(Category Category) : IDomainEvent;