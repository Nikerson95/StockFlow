using StockFlow.Application.Repositories;

namespace StockFlow.Application.Categories.Update;

public class UpdateCategoryUseCase
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryUseCase(
        ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<UpdateCategoryResponse?> ExecuteAsync(
        Guid id,
        UpdateCategoryRequest request,
        CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(
            id,
            cancellationToken);

        if (category is null)
        {
            return null;
        }

        var normalizedName = request.Name.Trim();

        var categoryWithSameNameExists =
            await _categoryRepository.ExistsByNameAsync(
                normalizedName,
                cancellationToken);

        if (categoryWithSameNameExists &&
            !string.Equals(
                category.Name,
                normalizedName,
                StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                "Já existe uma categoria com esse nome.");
        }

        category.Update(
            normalizedName,
            request.Description);

        _categoryRepository.Update(category);

        await _categoryRepository.SaveChangesAsync(
            cancellationToken);

        return new UpdateCategoryResponse(
            category.Id,
            category.Name,
            category.Description);
    }
}