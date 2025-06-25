namespace Catalog.Categories.EventHandlers;

public class CategoryCreatedEventHandler(ILogger<CategoryCreatedEventHandler> logger) : INotificationHandler<CategoryCreatedEvent>
{
    public Task Handle(CategoryCreatedEvent notification, CancellationToken cancellationToken)
    {
        // Handle the category created event here
        logger.LogInformation("Domain event handled: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}