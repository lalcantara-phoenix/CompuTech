using FluentValidation;

namespace CompuTech.Application.Equipment.Commands.UpdateEquipment;

public class UpdateEquipmentCommandValidator : AbstractValidator<UpdateEquipmentCommand>
{
    public UpdateEquipmentCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El Id debe ser mayor que 0.");

        RuleFor(x => x.Brand)
            .NotEmpty().WithMessage("La marca es requerida.")
            .MaximumLength(100).WithMessage("La marca no puede superar 100 caracteres.");

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("El modelo es requerido.")
            .MaximumLength(100).WithMessage("El modelo no puede superar 100 caracteres.");
    }
}
