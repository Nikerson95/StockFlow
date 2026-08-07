using StockFlow.Application.Repositories;

namespace StockFlow.Application.Products.Delete;

public class DeleteProductUseCase
{
    private readonly IProductRepository _productRepository;

    public DeleteProductUseCase(
        IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<bool> ExecuteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(
            id,
            cancellationToken);

        if (product is null)
        {
            return false;
        }

        _productRepository.Delete(product);

        await _productRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}