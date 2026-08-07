using StockFlow.Application.Repositories;

namespace StockFlow.Application.Products.LowStock;

public class GetLowStockProductsUseCase
{
    private readonly IProductRepository _productRepository;

    public GetLowStockProductsUseCase(
        IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IReadOnlyList<GetLowStockProductsResponse>> ExecuteAsync(
        CancellationToken cancellationToken = default)
    {
        var products = await _productRepository.GetAllAsync(
            cancellationToken);

        return products
            .Where(product => product.IsBelowMinimumStock())
            .Select(product => new GetLowStockProductsResponse(
                product.Id,
                product.Name,
                product.Quantity,
                product.MinimumStock,
                product.CategoryId,
                product.Category.Name))
            .ToList();
    }
}