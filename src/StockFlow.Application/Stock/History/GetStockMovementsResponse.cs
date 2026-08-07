namespace StockFlow.Application.Stock.History;

public record GetStockMovementsResponse(
    Guid Id,
    string Type,
    int Quantity,
    string Reason,
    DateTime CreatedAt);