namespace StockFlow.Application.Categories.Update;

public record UpdateCategoryResponse(
    Guid Id,
    string Name,
    string Description);