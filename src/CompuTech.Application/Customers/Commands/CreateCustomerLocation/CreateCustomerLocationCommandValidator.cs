using FluentValidation;

namespace CompuTech.Application.Customers.Commands.CreateCustomerLocation;

public class CreateCustomerLocationCommandValidator : AbstractValidator<CreateCustomerLocationCommand>
{
    public CreateCustomerLocationCommandValidator()
    {
        RuleFor(x => x.CustomerId).GreaterThan(0);

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre de la ubicación es requerido.")
            .MaximumLength(150).WithMessage("El nombre no puede superar 150 caracteres.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("La dirección es requerida.")
            .MaximumLength(300).WithMessage("La dirección no puede superar 300 caracteres.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("La ciudad es requerida.")
            .MaximumLength(100).WithMessage("La ciudad no puede superar 100 caracteres.");

        RuleFor(x => x.ContactPhone)
            .MaximumLength(20).WithMessage("El teléfono no puede superar 20 caracteres.")
            .When(x => x.ContactPhone is not null);
    }
}
