namespace StockFlow.Application.Categories.Create;

public record CreateCategoryResponse(
    Guid Id,
    string Name,
    string Description);