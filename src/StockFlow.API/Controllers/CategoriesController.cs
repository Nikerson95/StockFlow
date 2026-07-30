using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Categories.Create;
using StockFlow.Application.Categories.GetAll;
using StockFlow.Application.Categories.GetById;
using StockFlow.Application.Categories.Update;
namespace StockFlow.API.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly CreateCategoryUseCase _createCategoryUseCase;
private readonly GetAllCategoriesUseCase _getAllCategoriesUseCase;
private readonly GetCategoryByIdUseCase _getCategoryByIdUseCase;

private readonly UpdateCategoryUseCase _updateCategoryUseCase;

    public CategoriesController(
    CreateCategoryUseCase createCategoryUseCase,
    GetAllCategoriesUseCase getAllCategoriesUseCase,
    GetCategoryByIdUseCase getCategoryByIdUseCase,
    UpdateCategoryUseCase updateCategoryUseCase)
{
    _createCategoryUseCase = createCategoryUseCase;
    _getAllCategoriesUseCase = getAllCategoriesUseCase;
    _getCategoryByIdUseCase = getCategoryByIdUseCase;
    _updateCategoryUseCase = updateCategoryUseCase;
}



    [HttpPost]
    [ProducesResponseType<CreateCategoryResponse>(
        StatusCodes.Status201Created)]
    [ProducesResponseType(
        StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateCategoryResponse>> CreateAsync(
        CreateCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _createCategoryUseCase.ExecuteAsync(
            request,
            cancellationToken);

        return StatusCode(
            StatusCodes.Status201Created,
            response);
    }

    [HttpGet]
[ProducesResponseType<IReadOnlyList<GetAllCategoriesResponse>>(
    StatusCodes.Status200OK)]
public async Task<ActionResult<IReadOnlyList<GetAllCategoriesResponse>>>
    GetAllAsync(CancellationToken cancellationToken)
{
    var response = await _getAllCategoriesUseCase.ExecuteAsync(
        cancellationToken);

    return Ok(response);
}

[HttpGet("{id:guid}")]
[ProducesResponseType<GetCategoryByIdResponse>(
    StatusCodes.Status200OK)]
[ProducesResponseType(
    StatusCodes.Status404NotFound)]
public async Task<ActionResult<GetCategoryByIdResponse>> GetByIdAsync(
    Guid id,
    CancellationToken cancellationToken)
{
    var response = await _getCategoryByIdUseCase.ExecuteAsync(
        id,
        cancellationToken);

    if (response is null)
    {
        return NotFound();
    }

    return Ok(response);
}

[HttpPut("{id:guid}")]
[ProducesResponseType<UpdateCategoryResponse>(
    StatusCodes.Status200OK)]
[ProducesResponseType(
    StatusCodes.Status404NotFound)]
[ProducesResponseType(
    StatusCodes.Status400BadRequest)]
public async Task<ActionResult<UpdateCategoryResponse>> UpdateAsync(
    Guid id,
    UpdateCategoryRequest request,
    CancellationToken cancellationToken)
{
    var response = await _updateCategoryUseCase.ExecuteAsync(
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