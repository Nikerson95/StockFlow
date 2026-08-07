using StockFlow.Domain.Enums;

namespace StockFlow.Domain.Entities;

public class StockMovement
{
    public Guid Id { get; private set; }

    public Guid ProductId { get; private set; }

    public Product Product { get; private set; } = null!;

    public StockMovementType Type { get; private set; }

    public int Quantity { get; private set; }

    public string Reason { get; private set; } = string.Empty;

    public DateTime CreatedAt { get; private set; }

    private StockMovement()
    {
    }

    public StockMovement(
        Guid productId,
        StockMovementType type,
        int quantity,
        string reason)
    {
        ValidateProductId(productId);
        ValidateQuantity(quantity);

        Id = Guid.NewGuid();
        ProductId = productId;
        Type = type;
        Quantity = quantity;
        Reason = reason?.Trim() ?? string.Empty;
        CreatedAt = DateTime.UtcNow;
    }

    private static void ValidateProductId(Guid productId)
    {
        if (productId == Guid.Empty)
        {
            throw new ArgumentException(
                "O produto é obrigatório.",
                nameof(productId));
        }
    }

    private static void ValidateQuantity(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(quantity),
                "A quantidade da movimentação deve ser maior que zero.");
        }
    }
}