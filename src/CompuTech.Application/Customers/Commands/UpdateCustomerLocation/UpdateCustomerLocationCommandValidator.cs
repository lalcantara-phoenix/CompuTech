using FluentValidation;

namespace CompuTech.Application.Customers.Commands.UpdateCustomerLocation;

public class UpdateCustomerLocationCommandValidator : AbstractValidator<UpdateCustomerLocationCommand>
{
    public UpdateCustomerLocationCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre de la ubicación es requerido.")
            .MaximumLength(150);

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("La dirección es requerida.")
            .MaximumLength(300);

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("La ciudad es requerida.")
            .MaximumLength(100);

        RuleFor(x => x.ContactPhone)
            .MaximumLength(20)
            .When(x => x.ContactPhone is not null);
    }
}
