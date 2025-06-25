namespace Catalog.Products.EventHandlers;

public class ProductUpdatedEventHandler(ILogger<ProductUpdatedEventHandler> logger) : INotificationHandler<ProductUpdatedEvent>
{
    public Task Handle(ProductUpdatedEvent notification, CancellationToken cancellationToken)
    {
        // Handle the product updated event here
        logger.LogInformation("Domain event handled: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}