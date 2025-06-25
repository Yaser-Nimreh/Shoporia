namespace Catalog.Categories.Models;

public class Category : Aggregate<Guid>
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public ICollection<Product> Products { get; private set; } = [];

    public static Category Create(Guid id, string name, string description)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        
        var category = new Category
        {
            Id = id,
            Name = name,
            Description = description
        };

        category.AddDomainEvent(new CategoryCreatedEvent(category));

        return category;
    }

    public void Update(string name, string description)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        
        Name = name;
        Description = description;

        AddDomainEvent(new CategoryUpdatedEvent(this));
    }
}