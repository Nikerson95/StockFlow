using StockFlow.Application.Repositories;

namespace StockFlow.Application.Categories.GetById;

public class GetCategoryByIdUseCase
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryByIdUseCase(
        ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<GetCategoryByIdResponse?> ExecuteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(
            id,
            cancellationToken);

        if (category is null)
        {
            return null;
        }

        return new GetCategoryByIdResponse(
            category.Id,
            category.Name,
            category.Description);
    }
}