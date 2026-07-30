using StockFlow.Application.Repositories;

namespace StockFlow.Application.Categories.Delete;

public class DeleteCategoryUseCase
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryUseCase(
        ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<bool> ExecuteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(
            id,
            cancellationToken);

        if (category is null)
        {
            return false;
        }

        _categoryRepository.Delete(category);

        await _categoryRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}