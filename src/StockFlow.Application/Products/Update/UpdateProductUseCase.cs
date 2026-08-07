using StockFlow.Application.Repositories;

namespace StockFlow.Application.Products.Update;

public class UpdateProductUseCase
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public UpdateProductUseCase(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<UpdateProductResponse?> ExecuteAsync(
        Guid id,
        UpdateProductRequest request,
        CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(
            id,
            cancellationToken);

        if (product is null)
        {
            return null;
        }

        var category = await _categoryRepository.GetByIdAsync(
            request.CategoryId,
            cancellationToken);

        if (category is null)
        {
            throw new InvalidOperationException(
                "A categoria informada não existe.");
        }

        product.Update(
            request.Name,
            request.Description,
            request.Price,
            request.MinimumStock,
            request.CategoryId);

        _productRepository.Update(product);

        await _productRepository.SaveChangesAsync(
            cancellationToken);

        return new UpdateProductResponse(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.Quantity,
            product.MinimumStock,
            product.CategoryId,
            category.Name);
    }
}