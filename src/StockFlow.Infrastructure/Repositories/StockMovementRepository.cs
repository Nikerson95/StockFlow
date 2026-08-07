using Microsoft.EntityFrameworkCore;
using StockFlow.Application.Repositories;
using StockFlow.Domain.Entities;
using StockFlow.Infrastructure.Persistence;

namespace StockFlow.Infrastructure.Repositories;

public class StockMovementRepository : IStockMovementRepository
{
    private readonly StockFlowDbContext _dbContext;

    public StockMovementRepository(
        StockFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        StockMovement movement,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.StockMovements.AddAsync(
            movement,
            cancellationToken);
    }

    public async Task<IReadOnlyList<StockMovement>> GetByProductIdAsync(
        Guid productId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.StockMovements
            .AsNoTracking()
            .Where(movement => movement.ProductId == productId)
            .OrderByDescending(movement => movement.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<StockMovement>> GetAllAsync(
    CancellationToken cancellationToken = default)
{
    return await _dbContext.StockMovements
        .AsNoTracking()
        .Include(movement => movement.Product)
        .OrderByDescending(movement => movement.CreatedAt)
        .ToListAsync(cancellationToken);
}

}