using FluentValidation;
using CompuTech.Domain.Enums;

namespace CompuTech.Application.ServiceOrders.Commands.AddServiceOrderItem;

public class AddServiceOrderItemCommandValidator : AbstractValidator<AddServiceOrderItemCommand>
{
    public AddServiceOrderItemCommandValidator()
    {
        RuleFor(x => x.ServiceOrderId)
            .GreaterThan(0);

        RuleFor(x => x.ItemType)
            .NotEmpty()
            .Must(t => Enum.TryParse<ServiceOrderItemType>(t, out _)).WithMessage("Invalid ItemType");

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.Quantity)
            .GreaterThan(0);

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0m);

        RuleFor(x => x.InventoryItemId)
            .NotNull()
            .When(x => x.ItemType == "Part")
            .WithMessage("InventoryItemId is required for Part items");

        RuleFor(x => x.InventoryItemId)
            .Null()
            .When(x => x.ItemType == "Labor")
            .WithMessage("InventoryItemId must be null for Labor items");
    }
}
