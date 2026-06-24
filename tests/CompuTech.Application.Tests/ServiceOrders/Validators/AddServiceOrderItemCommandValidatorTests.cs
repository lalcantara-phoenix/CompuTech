using CompuTech.Application.ServiceOrders.Commands.AddServiceOrderItem;
using FluentValidation.TestHelper;
using Xunit;

namespace CompuTech.Application.Tests.ServiceOrders.Validators;

public class AddServiceOrderItemCommandValidatorTests
{
    private readonly AddServiceOrderItemCommandValidator _validator = new();

    [Fact]
    public void Rule3_Part_WithInventoryItemId_ShouldPass()
    {
        var cmd = new AddServiceOrderItemCommand(1, "Part", "RAM 8GB", 2, 50m, InventoryItemId: 10);
        var result = _validator.TestValidate(cmd);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Rule3_Part_WithoutInventoryItemId_ShouldFail()
    {
        var cmd = new AddServiceOrderItemCommand(1, "Part", "RAM 8GB", 2, 50m, InventoryItemId: null);
        var result = _validator.TestValidate(cmd);
        result.ShouldHaveValidationErrorFor(x => x.InventoryItemId)
            .WithErrorMessage("InventoryItemId is required for Part items");
    }

    [Fact]
    public void Rule4_Labor_WithoutInventoryItemId_ShouldPass()
    {
        var cmd = new AddServiceOrderItemCommand(1, "Labor", "Technician hour", 3, 75m, InventoryItemId: null);
        var result = _validator.TestValidate(cmd);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Rule4_Labor_WithInventoryItemId_ShouldFail()
    {
        var cmd = new AddServiceOrderItemCommand(1, "Labor", "Technician hour", 3, 75m, InventoryItemId: 5);
        var result = _validator.TestValidate(cmd);
        result.ShouldHaveValidationErrorFor(x => x.InventoryItemId)
            .WithErrorMessage("InventoryItemId must be null for Labor items");
    }

    [Fact]
    public void Quantity_Zero_ShouldFail()
    {
        var cmd = new AddServiceOrderItemCommand(1, "Labor", "Technician hour", 0, 75m);
        var result = _validator.TestValidate(cmd);
        result.ShouldHaveValidationErrorFor(x => x.Quantity);
    }

    [Fact]
    public void UnitPrice_Zero_ShouldFail()
    {
        var cmd = new AddServiceOrderItemCommand(1, "Labor", "Technician hour", 1, 0m);
        var result = _validator.TestValidate(cmd);
        result.ShouldHaveValidationErrorFor(x => x.UnitPrice);
    }
}
