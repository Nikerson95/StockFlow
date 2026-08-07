using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Products.Create;
using StockFlow.Application.Products.GetAll;

namespace StockFlow.API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly CreateProductUseCase _createProductUseCase;
    private readonly GetAllProductsUseCase _getAllProductsUseCase;

    public ProductsController(
    CreateProductUseCase createProductUseCase,
    GetAllProductsUseCase getAllProductsUseCase)
{
    _createProductUseCase = createProductUseCase;
    _getAllProductsUseCase = getAllProductsUseCase;
}

    [HttpPost]
    [ProducesResponseType<CreateProductResponse>(
        StatusCodes.Status201Created)]
    [ProducesResponseType(
        StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateProductResponse>> CreateAsync(
        CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _createProductUseCase.ExecuteAsync(
            request,
            cancellationToken);

        return StatusCode(
            StatusCodes.Status201Created,
            response);
    }

    [HttpGet]
[ProducesResponseType<IReadOnlyList<GetAllProductsResponse>>(
    StatusCodes.Status200OK)]
public async Task<ActionResult<IReadOnlyList<GetAllProductsResponse>>>
    GetAllAsync(CancellationToken cancellationToken)
{
    var response = await _getAllProductsUseCase.ExecuteAsync(
        cancellationToken);

    return Ok(response);
}

}