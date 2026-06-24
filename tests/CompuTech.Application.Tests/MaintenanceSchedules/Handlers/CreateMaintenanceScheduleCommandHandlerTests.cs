using CompuTech.Application.MaintenanceSchedules.Commands.CreateMaintenanceSchedule;
using CompuTech.Domain.Enums;
using CompuTech.Domain.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;
using EquipmentEntity = CompuTech.Domain.Entities.Equipment;
using MaintenanceScheduleEntity = CompuTech.Domain.Entities.MaintenanceSchedule;

namespace CompuTech.Application.Tests.MaintenanceSchedules.Handlers;

public class CreateMaintenanceScheduleCommandHandlerTests
{
    private readonly Mock<IMaintenanceScheduleRepository> _scheduleRepo = new();
    private readonly Mock<IEquipmentRepository> _equipmentRepo = new();
    private readonly CreateMaintenanceScheduleCommandHandler _handler;

    public CreateMaintenanceScheduleCommandHandlerTests()
    {
        _handler = new CreateMaintenanceScheduleCommandHandler(_scheduleRepo.Object, _equipmentRepo.Object);
    }

    private static EquipmentEntity ActiveEquipment() =>
        EquipmentEntity.Create(1, 1, "SN-001", "Dell", "Latitude", EquipmentType.Laptop);

    private static CreateMaintenanceScheduleCommand ValidCommand() =>
        new(1, "Monthly", DateTime.UtcNow.AddMonths(1));

    [Fact]
    public async Task Rule1_ActiveScheduleExists_ThrowsInvalidOperationException()
    {
        var equipment = ActiveEquipment();
        var existingSchedule = MaintenanceScheduleEntity.Create(1, MaintenanceFrequency.Monthly, DateTime.UtcNow.AddMonths(1));

        _equipmentRepo.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync(equipment);
        _scheduleRepo.Setup(r => r.GetByEquipmentIdAsync(1, default)).ReturnsAsync(existingSchedule);

        var act = () => _handler.Handle(ValidCommand(), default);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("*active maintenance schedule*");
    }

    [Fact]
    public async Task Rule1_NoExistingSchedule_CreatesSuccessfully()
    {
        var equipment = ActiveEquipment();
        _equipmentRepo.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync(equipment);
        _scheduleRepo.Setup(r => r.GetByEquipmentIdAsync(1, default)).ReturnsAsync((MaintenanceScheduleEntity?)null);
        _scheduleRepo.Setup(r => r.AddAsync(It.IsAny<MaintenanceScheduleEntity>(), default)).ReturnsAsync(1);

        var act = () => _handler.Handle(ValidCommand(), default);

        await act.Should().NotThrowAsync();
        _scheduleRepo.Verify(r => r.AddAsync(It.IsAny<MaintenanceScheduleEntity>(), default), Times.Once);
    }

    [Fact]
    public async Task Rule1_InactiveExistingSchedule_CreatesSuccessfully()
    {
        var equipment = ActiveEquipment();
        var inactiveSchedule = MaintenanceScheduleEntity.Create(1, MaintenanceFrequency.Monthly, DateTime.UtcNow.AddMonths(1));
        inactiveSchedule.Deactivate();

        _equipmentRepo.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync(equipment);
        _scheduleRepo.Setup(r => r.GetByEquipmentIdAsync(1, default)).ReturnsAsync(inactiveSchedule);
        _scheduleRepo.Setup(r => r.AddAsync(It.IsAny<MaintenanceScheduleEntity>(), default)).ReturnsAsync(1);

        var act = () => _handler.Handle(ValidCommand(), default);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task EquipmentNotFound_ThrowsKeyNotFoundException()
    {
        _equipmentRepo.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync((EquipmentEntity?)null);

        var act = () => _handler.Handle(ValidCommand(), default);

        await act.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task InactiveEquipment_ThrowsInvalidOperationException()
    {
        var equipment = ActiveEquipment();
        equipment.Deactivate();
        _equipmentRepo.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync(equipment);

        var act = () => _handler.Handle(ValidCommand(), default);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("*not active*");
    }
}
