namespace StockFlow.Application.Categories.GetById;

public record GetCategoryByIdResponse(
    Guid Id,
    string Name,
    string Description);