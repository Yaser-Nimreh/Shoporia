namespace Catalog.Products.Events;

public record ProductUpdatedEvent(Product Product) : IDomainEvent;