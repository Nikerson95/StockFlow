using FluentValidation;

namespace StockFlow.Application.Stock.Exit;

public class RemoveStockRequestValidator : AbstractValidator<RemoveStockRequest>
{
    public RemoveStockRequestValidator()
    {
        RuleFor(request => request.Quantity)
            .GreaterThan(0)
            .WithMessage("A quantidade deve ser maior que zero.");

        RuleFor(request => request.Reason)
            .NotEmpty()
            .WithMessage("O motivo da movimentação é obrigatório.")
            .MaximumLength(300)
            .WithMessage("O motivo deve possuir no máximo 300 caracteres.");
    }
}