namespace StockFlow.Application.Stock.Exit;

public record RemoveStockResponse(
    Guid ProductId,
    int QuantityRemoved,
    int CurrentQuantity,
    string Reason);