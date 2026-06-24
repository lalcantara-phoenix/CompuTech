using FluentValidation;

namespace CompuTech.Application.Technicians.Commands.UpdateTechnician;

public class UpdateTechnicianCommandValidator : AbstractValidator<UpdateTechnicianCommand>
{
    public UpdateTechnicianCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El Id debe ser mayor que 0.");

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("El nombre completo es requerido.")
            .MaximumLength(200).WithMessage("El nombre completo no puede superar 200 caracteres.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("El teléfono es requerido.")
            .MaximumLength(20).WithMessage("El teléfono no puede superar 20 caracteres.");

        RuleFor(x => x.Specialty)
            .NotEmpty().WithMessage("La especialidad es requerida.")
            .MaximumLength(100).WithMessage("La especialidad no puede superar 100 caracteres.");
    }
}
