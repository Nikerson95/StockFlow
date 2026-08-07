namespace StockFlow.Application.Stock.Entry;

public record AddStockRequest(
    int Quantity,
    string Reason);