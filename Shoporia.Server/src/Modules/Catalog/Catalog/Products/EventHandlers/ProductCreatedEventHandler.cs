namespace Catalog.Products.EventHandlers;

public class ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger) : INotificationHandler<ProductCreatedEvent>
{
    public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
    {
        // Handle the product created event here
        logger.LogInformation("Domain event handled: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}