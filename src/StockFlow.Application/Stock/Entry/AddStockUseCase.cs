using StockFlow.Application.Repositories;
using StockFlow.Domain.Entities;
using StockFlow.Domain.Enums;

namespace StockFlow.Application.Stock.Entry;

public class AddStockUseCase
{
    private readonly IProductRepository _productRepository;
    private readonly IStockMovementRepository _stockMovementRepository;

    public AddStockUseCase(
        IProductRepository productRepository,
        IStockMovementRepository stockMovementRepository)
    {
        _productRepository = productRepository;
        _stockMovementRepository = stockMovementRepository;
    }

    public async Task<AddStockResponse?> ExecuteAsync(
        Guid productId,
        AddStockRequest request,
        CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(
            productId,
            cancellationToken);

        if (product is null)
        {
            return null;
        }

        product.AddStock(request.Quantity);

        var movement = new StockMovement(
            product.Id,
            StockMovementType.Entry,
            request.Quantity,
            request.Reason);

        _productRepository.Update(product);

        await _stockMovementRepository.AddAsync(
            movement,
            cancellationToken);

        await _stockMovementRepository.SaveChangesAsync(
            cancellationToken);

        return new AddStockResponse(
            product.Id,
            request.Quantity,
            product.Quantity,
            movement.Reason);
    }
}