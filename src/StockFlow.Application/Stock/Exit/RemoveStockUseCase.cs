using StockFlow.Application.Repositories;
using StockFlow.Domain.Entities;
using StockFlow.Domain.Enums;

namespace StockFlow.Application.Stock.Exit;

public class RemoveStockUseCase
{
    private readonly IProductRepository _productRepository;
    private readonly IStockMovementRepository _stockMovementRepository;

    public RemoveStockUseCase(
        IProductRepository productRepository,
        IStockMovementRepository stockMovementRepository)
    {
        _productRepository = productRepository;
        _stockMovementRepository = stockMovementRepository;
    }

    public async Task<RemoveStockResponse?> ExecuteAsync(
        Guid productId,
        RemoveStockRequest request,
        CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(
            productId,
            cancellationToken);

        if (product is null)
        {
            return null;
        }

        product.RemoveStock(request.Quantity);

        var movement = new StockMovement(
            product.Id,
            StockMovementType.Exit,
            request.Quantity,
            request.Reason);

        _productRepository.Update(product);

        await _stockMovementRepository.AddAsync(
            movement,
            cancellationToken);

        await _stockMovementRepository.SaveChangesAsync(
            cancellationToken);

        return new RemoveStockResponse(
            product.Id,
            request.Quantity,
            product.Quantity,
            movement.Reason);
    }
}