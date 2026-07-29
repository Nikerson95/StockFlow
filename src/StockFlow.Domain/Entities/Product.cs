namespace StockFlow.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public decimal Price { get; private set; }

    public int Quantity { get; private set; }

    public int MinimumStock { get; private set; }

    private Product()
    {
        
        
    }

    public Product(
        string name,
        string description,
        decimal price,
        int quantity,
        int minimumStock)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Price = price;
        Quantity = quantity;
        MinimumStock = minimumStock;
    }
}