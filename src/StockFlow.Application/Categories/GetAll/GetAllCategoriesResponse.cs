namespace StockFlow.Application.Categories.GetAll;

public record GetAllCategoriesResponse(
    Guid Id,
    string Name,
    string Description);