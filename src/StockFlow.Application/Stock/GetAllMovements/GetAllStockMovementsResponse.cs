namespace StockFlow.Application.Stock.GetAllMovements;

public record GetAllStockMovementsResponse(
    Guid Id,
    Guid ProductId,
    string ProductName,
    string Type,
    int Quantity,
    string Reason,
    DateTime CreatedAt);