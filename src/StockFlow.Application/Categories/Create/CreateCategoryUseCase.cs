using StockFlow.Application.Repositories;
using StockFlow.Domain.Entities;

namespace StockFlow.Application.Categories.Create;

public class CreateCategoryUseCase
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryUseCase(
        ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CreateCategoryResponse> ExecuteAsync(
        CreateCategoryRequest request,
        CancellationToken cancellationToken = default)
    {
        var normalizedName = request.Name.Trim();

        var categoryAlreadyExists =
            await _categoryRepository.ExistsByNameAsync(
                normalizedName,
                cancellationToken);

        if (categoryAlreadyExists)
        {
            throw new InvalidOperationException(
                "Já existe uma categoria com esse nome.");
        }

        var category = new Category(
            normalizedName,
            request.Description);

        await _categoryRepository.AddAsync(
            category,
            cancellationToken);

        await _categoryRepository.SaveChangesAsync(
            cancellationToken);

        return new CreateCategoryResponse(
            category.Id,
            category.Name,
            category.Description);
    }
}
