using FluentValidation;
using CompuTech.Domain.Enums;

namespace CompuTech.Application.ServiceOrders.Commands.CreateServiceOrder;

public class CreateServiceOrderCommandValidator : AbstractValidator<CreateServiceOrderCommand>
{
    public CreateServiceOrderCommandValidator()
    {
        RuleFor(x => x.EquipmentId)
            .GreaterThan(0);

        RuleFor(x => x.TechnicianId)
            .GreaterThan(0);

        RuleFor(x => x.Type)
            .NotEmpty()
            .Must(t => Enum.TryParse<ServiceOrderType>(t, out _)).WithMessage("Invalid Type");

        RuleFor(x => x.SubType)
            .NotEmpty()
            .Must(s => Enum.TryParse<ServiceOrderSubType>(s, out _)).WithMessage("Invalid SubType");

        RuleFor(x => x.Priority)
            .NotEmpty()
            .Must(p => Enum.TryParse<ServiceOrderPriority>(p, out _)).WithMessage("Invalid Priority");

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(x => x)
            .Must(x =>
            {
                if (!Enum.TryParse<ServiceOrderType>(x.Type, out var type)) return true;
                if (!Enum.TryParse<ServiceOrderSubType>(x.SubType, out var subType)) return true;
                return type switch
                {
                    ServiceOrderType.Repair => subType is ServiceOrderSubType.Hardware
                        or ServiceOrderSubType.Software or ServiceOrderSubType.Diagnostic,
                    ServiceOrderType.Maintenance => subType is ServiceOrderSubType.Preventive
                        or ServiceOrderSubType.OnDemand,
                    _ => false
                };
            })
            .WithMessage("SubType is not valid for the specified Type");
    }
}
