using FluentValidation;

namespace CompuTech.Application.Inventory.Commands.AdjustStock;

public class AdjustStockCommandValidator : AbstractValidator<AdjustStockCommand>
{
    public AdjustStockCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El ID debe ser mayor que cero.");

        RuleFor(x => x.Delta)
            .NotEqual(0).WithMessage("El delta no puede ser cero.");
    }
}
