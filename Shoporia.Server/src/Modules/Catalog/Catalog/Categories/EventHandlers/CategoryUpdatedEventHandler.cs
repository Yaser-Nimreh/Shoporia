namespace Catalog.Categories.EventHandlers;

public class CategoryUpdatedEventHandler(ILogger<CategoryUpdatedEventHandler> logger) : INotificationHandler<CategoryUpdatedEvent>
{
    public Task Handle(CategoryUpdatedEvent notification, CancellationToken cancellationToken)
    {
        // Handle the category updated event here
        logger.LogInformation("Domain event handled: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}