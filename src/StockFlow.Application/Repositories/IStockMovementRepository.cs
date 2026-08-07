using StockFlow.Domain.Entities;

namespace StockFlow.Application.Repositories;

public interface IStockMovementRepository
{
    Task AddAsync(
        StockMovement movement,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<StockMovement>> GetByProductIdAsync(
        Guid productId,
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}