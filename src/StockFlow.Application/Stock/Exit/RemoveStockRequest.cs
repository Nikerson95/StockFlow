namespace StockFlow.Application.Stock.Exit;

public record RemoveStockRequest(
    int Quantity,
    string Reason);