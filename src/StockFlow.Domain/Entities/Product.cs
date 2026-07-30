namespace StockFlow.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public decimal Price { get; private set; }

    public int Quantity { get; private set; }

    public int MinimumStock { get; private set; }

    public Guid CategoryId { get; private set; }

public Category Category { get; private set; } = null!;

public Product(
    string name,
    string description,
    decimal price,
    int quantity,
    int minimumStock,
    Guid categoryId)
{
    ValidateName(name);
    ValidatePrice(price);
    ValidateQuantity(quantity);
    ValidateMinimumStock(minimumStock);
    ValidateCategoryId(categoryId);

    Id = Guid.NewGuid();
    Name = name.Trim();
    Description = description.Trim();
    Price = price;
    Quantity = quantity;
    MinimumStock = minimumStock;
    CategoryId = categoryId;
}
    

    public void AddStock(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(quantity),
                "A quantidade adicionada deve ser maior que zero.");
        }

        Quantity += quantity;
    }

    public void RemoveStock(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(quantity),
                "A quantidade removida deve ser maior que zero.");
        }

        if (quantity > Quantity)
        {
            throw new InvalidOperationException(
                "Não há estoque suficiente para realizar a saída.");
        }

        Quantity -= quantity;
    }

    public void ChangePrice(decimal newPrice)
    {
        ValidatePrice(newPrice);

        Price = newPrice;
    }

    public bool IsBelowMinimumStock()
    {
        return Quantity < MinimumStock;
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "O nome do produto é obrigatório.",
                nameof(name));
        }

        if (name.Length > 150)
        {
            throw new ArgumentException(
                "O nome do produto deve ter no máximo 150 caracteres.",
                nameof(name));
        }
    }

    private static void ValidatePrice(decimal price)
    {
        if (price < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(price),
                "O preço do produto não pode ser negativo.");
        }
    }

    private static void ValidateQuantity(int quantity)
    {
        if (quantity < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(quantity),
                "A quantidade do produto não pode ser negativa.");
        }
    }

    private static void ValidateMinimumStock(int minimumStock)
    {
        if (minimumStock < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(minimumStock),
                "O estoque mínimo não pode ser negativo.");
        }
    }

    private static void ValidateCategoryId(Guid categoryId)
{
    if (categoryId == Guid.Empty)
    {
        throw new ArgumentException(
            "A categoria do produto é obrigatória.",
            nameof(categoryId));
    }
}
}