namespace StockFlow.Application.Products.Create;

public record CreateProductRequest(
    string Name,
    string Description,
    decimal Price,
    int Quantity,
    int MinimumStock,
    Guid CategoryId);