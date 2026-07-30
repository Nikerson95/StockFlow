namespace StockFlow.Application.Products.Create;

public record CreateProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    int Quantity,
    int MinimumStock,
    Guid CategoryId);