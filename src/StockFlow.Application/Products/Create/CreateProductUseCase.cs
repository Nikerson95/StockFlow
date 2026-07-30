using StockFlow.Application.Repositories;
using StockFlow.Domain.Entities;

namespace StockFlow.Application.Products.Create;

public class CreateProductUseCase
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public CreateProductUseCase(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<CreateProductResponse> ExecuteAsync(
        CreateProductRequest request,
        CancellationToken cancellationToken = default)
    {
        var normalizedName = request.Name.Trim();

        var productAlreadyExists =
            await _productRepository.ExistsByNameAsync(
                normalizedName,
                cancellationToken);

        if (productAlreadyExists)
        {
            throw new InvalidOperationException(
                "Já existe um produto com esse nome.");
        }

        var category = await _categoryRepository.GetByIdAsync(
            request.CategoryId,
            cancellationToken);

        if (category is null)
        {
            throw new InvalidOperationException(
                "A categoria informada não existe.");
        }

        var product = new Product(
            normalizedName,
            request.Description,
            request.Price,
            request.Quantity,
            request.MinimumStock,
            request.CategoryId);

        await _productRepository.AddAsync(
            product,
            cancellationToken);

        await _productRepository.SaveChangesAsync(
            cancellationToken);

        return new CreateProductResponse(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.Quantity,
            product.MinimumStock,
            product.CategoryId);
    }
}