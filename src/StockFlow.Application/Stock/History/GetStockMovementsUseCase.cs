using StockFlow.Application.Repositories;

namespace StockFlow.Application.Stock.History;

public class GetStockMovementsUseCase
{
    private readonly IProductRepository _productRepository;
    private readonly IStockMovementRepository _stockMovementRepository;

    public GetStockMovementsUseCase(
        IProductRepository productRepository,
        IStockMovementRepository stockMovementRepository)
    {
        _productRepository = productRepository;
        _stockMovementRepository = stockMovementRepository;
    }

    public async Task<IReadOnlyList<GetStockMovementsResponse>?> ExecuteAsync(
        Guid productId,
        CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(
            productId,
            cancellationToken);

        if (product is null)
        {
            return null;
        }

        var movements =
            await _stockMovementRepository.GetByProductIdAsync(
                productId,
                cancellationToken);

        return movements
            .Select(movement => new GetStockMovementsResponse(
                movement.Id,
                movement.Type.ToString(),
                movement.Quantity,
                movement.Reason,
                movement.CreatedAt))
            .ToList();
    }
}