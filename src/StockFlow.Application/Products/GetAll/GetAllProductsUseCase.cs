using StockFlow.Application.Repositories;

namespace StockFlow.Application.Products.GetAll;

public class GetAllProductsUseCase
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsUseCase(
        IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IReadOnlyList<GetAllProductsResponse>> ExecuteAsync(
        CancellationToken cancellationToken = default)
    {
        var products = await _productRepository.GetAllAsync(
            cancellationToken);

        return products
            .Select(product => new GetAllProductsResponse(
                product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.Quantity,
                product.MinimumStock,
                product.CategoryId,
                product.Category.Name))
            .ToList();
    }
}