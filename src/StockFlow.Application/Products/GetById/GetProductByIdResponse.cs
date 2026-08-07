namespace StockFlow.Application.Products.GetById;

public record GetProductByIdResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    int Quantity,
    int MinimumStock,
    Guid CategoryId,
    string CategoryName);