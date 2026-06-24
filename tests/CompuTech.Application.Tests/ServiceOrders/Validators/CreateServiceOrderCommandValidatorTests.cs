using CompuTech.Application.ServiceOrders.Commands.CreateServiceOrder;
using FluentValidation.TestHelper;
using Xunit;

namespace CompuTech.Application.Tests.ServiceOrders.Validators;

public class CreateServiceOrderCommandValidatorTests
{
    private readonly CreateServiceOrderCommandValidator _validator = new();

    private static CreateServiceOrderCommand ValidCommand(string type = "Repair", string subType = "Hardware") =>
        new(1, 1, type, subType, "Medium", "Test order description");

    [Theory]
    [InlineData("Repair", "Hardware")]
    [InlineData("Repair", "Software")]
    [InlineData("Repair", "Diagnostic")]
    [InlineData("Maintenance", "Preventive")]
    [InlineData("Maintenance", "OnDemand")]
    public void Rule5_ValidTypeSubTypeCombinations_ShouldPass(string type, string subType)
    {
        var result = _validator.TestValidate(ValidCommand(type, subType));

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("Repair", "Preventive")]
    [InlineData("Repair", "OnDemand")]
    [InlineData("Maintenance", "Hardware")]
    [InlineData("Maintenance", "Software")]
    [InlineData("Maintenance", "Diagnostic")]
    public void Rule5_InvalidTypeSubTypeCombinations_ShouldFail(string type, string subType)
    {
        var result = _validator.TestValidate(ValidCommand(type, subType));

        result.ShouldHaveValidationErrorFor(x => x)
            .WithErrorMessage("SubType is not valid for the specified Type");
    }

    [Fact]
    public void EquipmentId_Zero_ShouldFail()
    {
        var cmd = new CreateServiceOrderCommand(0, 1, "Repair", "Hardware", "Medium", "Test");
        var result = _validator.TestValidate(cmd);
        result.ShouldHaveValidationErrorFor(x => x.EquipmentId);
    }

    [Fact]
    public void Description_Empty_ShouldFail()
    {
        var cmd = new CreateServiceOrderCommand(1, 1, "Repair", "Hardware", "Medium", "");
        var result = _validator.TestValidate(cmd);
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Fact]
    public void Type_Invalid_ShouldFail()
    {
        var cmd = new CreateServiceOrderCommand(1, 1, "InvalidType", "Hardware", "Medium", "Test");
        var result = _validator.TestValidate(cmd);
        result.ShouldHaveValidationErrorFor(x => x.Type);
    }
}
