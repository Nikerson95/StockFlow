using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Stock.GetAllMovements;

namespace StockFlow.API.Controllers;

[ApiController]
[Route("api/stock")]
public class StockController : ControllerBase
{
    private readonly GetAllStockMovementsUseCase _getAllStockMovementsUseCase;

    public StockController(
        GetAllStockMovementsUseCase getAllStockMovementsUseCase)
    {
        _getAllStockMovementsUseCase = getAllStockMovementsUseCase;
    }

    [HttpGet("movements")]
    [ProducesResponseType<IReadOnlyList<GetAllStockMovementsResponse>>(
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<GetAllStockMovementsResponse>>>
        GetAllMovementsAsync(
            CancellationToken cancellationToken)
    {
        var response = await _getAllStockMovementsUseCase.ExecuteAsync(
            cancellationToken);

        return Ok(response);
    }
}