using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Products.Create;
using StockFlow.Application.Products.GetAll;
using StockFlow.Application.Products.GetById;
using StockFlow.Application.Products.Update;

namespace StockFlow.API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly CreateProductUseCase _createProductUseCase;
    private readonly GetAllProductsUseCase _getAllProductsUseCase;
    private readonly GetProductByIdUseCase _getProductByIdUseCase;
    private readonly UpdateProductUseCase _updateProductUseCase;

    public ProductsController(
    CreateProductUseCase createProductUseCase,
    GetAllProductsUseCase getAllProductsUseCase,
    GetProductByIdUseCase getProductByIdUseCase,
    UpdateProductUseCase updateProductUseCase)
{
    _createProductUseCase = createProductUseCase;
    _getAllProductsUseCase = getAllProductsUseCase;
    _getProductByIdUseCase = getProductByIdUseCase;
    _updateProductUseCase = updateProductUseCase;
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

[HttpGet("{id:guid}")]
[ProducesResponseType<GetProductByIdResponse>(
    StatusCodes.Status200OK)]
[ProducesResponseType(
    StatusCodes.Status404NotFound)]
public async Task<ActionResult<GetProductByIdResponse>> GetByIdAsync(
    Guid id,
    CancellationToken cancellationToken)
{
    var response = await _getProductByIdUseCase.ExecuteAsync(
        id,
        cancellationToken);

    if (response is null)
    {
        return NotFound();
    }

    return Ok(response);
}

[HttpPut("{id:guid}")]
[ProducesResponseType<UpdateProductResponse>(
    StatusCodes.Status200OK)]
[ProducesResponseType(
    StatusCodes.Status404NotFound)]
[ProducesResponseType(
    StatusCodes.Status400BadRequest)]
public async Task<ActionResult<UpdateProductResponse>> UpdateAsync(
    Guid id,
    UpdateProductRequest request,
    CancellationToken cancellationToken)
{
    var response = await _updateProductUseCase.ExecuteAsync(
        id,
        request,
        cancellationToken);

    if (response is null)
    {
        return NotFound();
    }

    return Ok(response);
}

}