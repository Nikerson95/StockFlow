using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Categories.Create;
using StockFlow.Application.Categories.GetAll;

namespace StockFlow.API.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly CreateCategoryUseCase _createCategoryUseCase;
private readonly GetAllCategoriesUseCase _getAllCategoriesUseCase;

    public CategoriesController(
    CreateCategoryUseCase createCategoryUseCase,
    GetAllCategoriesUseCase getAllCategoriesUseCase)
{
    _createCategoryUseCase = createCategoryUseCase;
    _getAllCategoriesUseCase = getAllCategoriesUseCase;
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
}