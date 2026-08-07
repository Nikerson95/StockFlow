namespace StockFlow.Application.Products.Update;

public record UpdateProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    int Quantity,
    int MinimumStock,
    Guid CategoryId,
    string CategoryName);