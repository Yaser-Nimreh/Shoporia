namespace Catalog.Products.Models;

public class Product : Aggregate<Guid>
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category? Category { get; private set; }
    public string ImageFilePath { get; private set; } = string.Empty;

    public static Product Create(Guid id, string name, string description, decimal price, Guid categoryId, string imageFilePath)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        
        var product = new Product
        {
            Id = id,
            Name = name,
            Description = description,
            Price = price,
            CategoryId = categoryId,
            ImageFilePath = imageFilePath
        };

        product.AddDomainEvent(new ProductCreatedEvent(product));

        return product;
    }

    public void Update(string name, string description, decimal price, Guid categoryId, string imageFilePath)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        
        Name = name;
        Description = description;
        CategoryId = categoryId;
        ImageFilePath = imageFilePath;

        if (Price != price)
        {
            Price = price;
            AddDomainEvent(new ProductPriceChangedEvent(this));
        }

        AddDomainEvent(new ProductUpdatedEvent(this));
    }
}