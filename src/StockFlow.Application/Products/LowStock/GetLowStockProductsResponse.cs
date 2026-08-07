namespace StockFlow.Application.Products.LowStock;

public record GetLowStockProductsResponse(
    Guid Id,
    string Name,
    int Quantity,
    int MinimumStock,
    Guid CategoryId,
    string CategoryName);