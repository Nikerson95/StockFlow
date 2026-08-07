using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace StockFlow.API.ExceptionHandlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var problemDetails = exception switch
{
    ArgumentOutOfRangeException => new ProblemDetails
    {
        Status = StatusCodes.Status400BadRequest,
        Title = "Valor inválido",
        Detail = exception.Message
    },

    ArgumentException => new ProblemDetails
    {
        Status = StatusCodes.Status400BadRequest,
        Title = "Dados inválidos",
        Detail = exception.Message
    },

    InvalidOperationException => new ProblemDetails
    {
        Status = StatusCodes.Status400BadRequest,
        Title = "Operação inválida",
        Detail = exception.Message
    },

    _ => new ProblemDetails
    {
        Status = StatusCodes.Status500InternalServerError,
        Title = "Erro interno",
        Detail = "Ocorreu um erro inesperado."
    }
};

        httpContext.Response.StatusCode =
            problemDetails.Status
            ?? StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(
            problemDetails,
            cancellationToken);

        return true;
    }
}