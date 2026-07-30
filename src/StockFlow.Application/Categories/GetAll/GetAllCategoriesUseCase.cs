using StockFlow.Application.Repositories;

namespace StockFlow.Application.Categories.GetAll;

public class GetAllCategoriesUseCase
{
    private readonly ICategoryRepository _categoryRepository;

    public GetAllCategoriesUseCase(
        ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IReadOnlyList<GetAllCategoriesResponse>> ExecuteAsync(
        CancellationToken cancellationToken = default)
    {
        var categories = await _categoryRepository.GetAllAsync(
            cancellationToken);

        return categories
            .Select(category => new GetAllCategoriesResponse(
                category.Id,
                category.Name,
                category.Description))
            .ToList();
    }
}