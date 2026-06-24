using FluentValidation;

namespace CompuTech.Application.Equipment.Commands.RegisterEquipment;

public class RegisterEquipmentCommandValidator : AbstractValidator<RegisterEquipmentCommand>
{
    public RegisterEquipmentCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("CustomerId debe ser mayor que 0.");

        RuleFor(x => x.LocationId)
            .GreaterThan(0).WithMessage("LocationId debe ser mayor que 0.");

        RuleFor(x => x.SerialNumber)
            .NotEmpty().WithMessage("El número de serie es requerido.")
            .MaximumLength(100).WithMessage("El número de serie no puede superar 100 caracteres.");

        RuleFor(x => x.Brand)
            .NotEmpty().WithMessage("La marca es requerida.")
            .MaximumLength(100).WithMessage("La marca no puede superar 100 caracteres.");

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("El modelo es requerido.")
            .MaximumLength(100).WithMessage("El modelo no puede superar 100 caracteres.");

        RuleFor(x => x.OperatingSystem)
            .MaximumLength(100).WithMessage("El sistema operativo no puede superar 100 caracteres.")
            .When(x => !string.IsNullOrEmpty(x.OperatingSystem));

        RuleFor(x => x.Notes)
            .MaximumLength(500).WithMessage("Las notas no pueden superar 500 caracteres.")
            .When(x => !string.IsNullOrEmpty(x.Notes));
    }
}
