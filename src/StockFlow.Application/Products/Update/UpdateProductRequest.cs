namespace StockFlow.Application.Products.Update;

public record UpdateProductRequest(
    string Name,
    string Description,
    decimal Price,
    int MinimumStock,
    Guid CategoryId);