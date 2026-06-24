using CompuTech.Domain.Interfaces;
using FluentValidation;

namespace CompuTech.Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator(ICustomerRepository repository)
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("El nombre es requerido.")
            .MaximumLength(200).WithMessage("El nombre no puede superar 200 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El correo es requerido.")
            .EmailAddress().WithMessage("El correo no tiene un formato válido.")
            .MaximumLength(150).WithMessage("El correo no puede superar 150 caracteres.")
            .MustAsync(async (email, ct) => !await repository.EmailExistsAsync(email, ct))
            .WithMessage("Ya existe un cliente con ese correo electrónico.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("El teléfono es requerido.")
            .MaximumLength(20).WithMessage("El teléfono no puede superar 20 caracteres.");

        RuleFor(x => x.TaxId)
            .MaximumLength(20).WithMessage("El RNC/cédula no puede superar 20 caracteres.")
            .When(x => x.TaxId is not null);
    }
}
