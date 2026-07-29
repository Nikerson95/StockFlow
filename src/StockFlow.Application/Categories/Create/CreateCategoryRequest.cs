namespace StockFlow.Application.Categories.Create;

public record CreateCategoryRequest(
    string Name,
    string Description);