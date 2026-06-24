using FluentValidation;
using CompuTech.Domain.Enums;

namespace CompuTech.Application.ServiceOrders.Commands.UpdateServiceOrder;

public class UpdateServiceOrderCommandValidator : AbstractValidator<UpdateServiceOrderCommand>
{
    public UpdateServiceOrderCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.TechnicianId)
            .GreaterThan(0);

        RuleFor(x => x.Priority)
            .NotEmpty()
            .Must(p => Enum.TryParse<ServiceOrderPriority>(p, out _)).WithMessage("Invalid Priority");

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(1000);
    }
}
