using CompuTech.Domain.Enums;
using FluentValidation;

namespace CompuTech.Application.MaintenanceSchedules.Commands.UpdateMaintenanceSchedule;

public class UpdateMaintenanceScheduleCommandValidator : AbstractValidator<UpdateMaintenanceScheduleCommand>
{
    public UpdateMaintenanceScheduleCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.Frequency)
            .NotEmpty()
            .Must(f => Enum.TryParse<MaintenanceFrequency>(f, out _))
            .WithMessage("Invalid Frequency");

        RuleFor(x => x.NextServiceDate)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("NextServiceDate must be in the future");
    }
}
