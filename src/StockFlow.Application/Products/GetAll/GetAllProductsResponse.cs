namespace StockFlow.Application.Products.GetAll;

public record GetAllProductsResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    int Quantity,
    int MinimumStock,
    Guid CategoryId,
    string CategoryName);