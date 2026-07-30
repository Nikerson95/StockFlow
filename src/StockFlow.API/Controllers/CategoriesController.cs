using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Categories.Create;
using StockFlow.Application.Categories.GetAll;
using StockFlow.Application.Categories.GetById;
namespace StockFlow.API.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly CreateCategoryUseCase _createCategoryUseCase;
private readonly GetAllCategoriesUseCase _getAllCategoriesUseCase;
private readonly GetCategoryByIdUseCase _getCategoryByIdUseCase;

    public CategoriesController(
    CreateCategoryUseCase createCategoryUseCase,
    GetAllCategoriesUseCase getAllCategoriesUseCase,
    GetCategoryByIdUseCase getCategoryByIdUseCase)
{
    _createCategoryUseCase = createCategoryUseCase;
    _getAllCategoriesUseCase = getAllCategoriesUseCase;
    _getCategoryByIdUseCase = getCategoryByIdUseCase;
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

}