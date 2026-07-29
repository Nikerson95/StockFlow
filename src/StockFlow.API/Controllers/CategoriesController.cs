using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Categories.Create;

namespace StockFlow.API.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly CreateCategoryUseCase _createCategoryUseCase;

    public CategoriesController(
        CreateCategoryUseCase createCategoryUseCase)
    {
        _createCategoryUseCase = createCategoryUseCase;
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
}