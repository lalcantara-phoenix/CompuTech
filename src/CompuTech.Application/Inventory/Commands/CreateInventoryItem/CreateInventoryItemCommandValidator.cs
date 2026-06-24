using FluentValidation;

namespace CompuTech.Application.Inventory.Commands.CreateInventoryItem;

public class CreateInventoryItemCommandValidator : AbstractValidator<CreateInventoryItemCommand>
{
    public CreateInventoryItemCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es requerido.")
            .MaximumLength(200).WithMessage("El nombre no puede superar 200 caracteres.");

        RuleFor(x => x.SKU)
            .NotEmpty().WithMessage("El SKU es requerido.")
            .MaximumLength(50).WithMessage("El SKU no puede superar 50 caracteres.");

        RuleFor(x => x.InitialStock)
            .GreaterThanOrEqualTo(0).WithMessage("El stock inicial no puede ser negativo.");

        RuleFor(x => x.SalePrice)
            .GreaterThan(0).WithMessage("El precio de venta debe ser mayor que cero.");
    }
}
