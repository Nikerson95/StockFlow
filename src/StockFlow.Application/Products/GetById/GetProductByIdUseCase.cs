using StockFlow.Application.Repositories;

namespace StockFlow.Application.Products.GetById;

public class GetProductByIdUseCase
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdUseCase(
        IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<GetProductByIdResponse?> ExecuteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(
            id,
            cancellationToken);

        if (product is null)
        {
            return null;
        }

        return new GetProductByIdResponse(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.Quantity,
            product.MinimumStock,
            product.CategoryId,
            product.Category.Name);
    }
}