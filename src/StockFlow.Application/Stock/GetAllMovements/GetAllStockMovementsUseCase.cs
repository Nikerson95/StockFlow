using StockFlow.Application.Repositories;

namespace StockFlow.Application.Stock.GetAllMovements;

public class GetAllStockMovementsUseCase
{
    private readonly IStockMovementRepository _stockMovementRepository;

    public GetAllStockMovementsUseCase(
        IStockMovementRepository stockMovementRepository)
    {
        _stockMovementRepository = stockMovementRepository;
    }

    public async Task<IReadOnlyList<GetAllStockMovementsResponse>> ExecuteAsync(
        CancellationToken cancellationToken = default)
    {
        var movements = await _stockMovementRepository.GetAllAsync(
            cancellationToken);

        return movements
            .Select(movement => new GetAllStockMovementsResponse(
                movement.Id,
                movement.ProductId,
                movement.Product.Name,
                movement.Type.ToString(),
                movement.Quantity,
                movement.Reason,
                movement.CreatedAt))
            .ToList();
    }
}