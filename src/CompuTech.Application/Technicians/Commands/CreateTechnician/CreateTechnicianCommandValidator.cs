using FluentValidation;

namespace CompuTech.Application.Technicians.Commands.CreateTechnician;

public class CreateTechnicianCommandValidator : AbstractValidator<CreateTechnicianCommand>
{
    public CreateTechnicianCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("El nombre completo es requerido.")
            .MaximumLength(200).WithMessage("El nombre completo no puede superar 200 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El correo es requerido.")
            .MaximumLength(200).WithMessage("El correo no puede superar 200 caracteres.")
            .EmailAddress().WithMessage("El correo no tiene un formato válido.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("El teléfono es requerido.")
            .MaximumLength(20).WithMessage("El teléfono no puede superar 20 caracteres.");

        RuleFor(x => x.Specialty)
            .NotEmpty().WithMessage("La especialidad es requerida.")
            .MaximumLength(100).WithMessage("La especialidad no puede superar 100 caracteres.");
    }
}
