namespace StockFlow.Application.Stock.Entry;

public record AddStockResponse(
    Guid ProductId,
    int QuantityAdded,
    int CurrentQuantity,
    string Reason);