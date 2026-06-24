using CompuTech.Application.ServiceOrders.Commands.CancelServiceOrder;
using CompuTech.Domain.Entities;
using CompuTech.Domain.Enums;
using CompuTech.Domain.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace CompuTech.Application.Tests.ServiceOrders.Handlers;

public class CancelServiceOrderCommandHandlerTests
{
    private readonly Mock<IServiceOrderRepository> _orderRepo = new();
    private readonly CancelServiceOrderCommandHandler _handler;

    public CancelServiceOrderCommandHandlerTests()
    {
        _handler = new CancelServiceOrderCommandHandler(_orderRepo.Object);
    }

    private static ServiceOrder PlannedOrder() =>
        ServiceOrder.Create("ORD-2026-0001", 1, 1,
            ServiceOrderType.Repair, ServiceOrderSubType.Hardware,
            ServiceOrderPriority.Medium, "Test order");

    [Fact]
    public async Task PlannedOrder_CanBeCancelled()
    {
        var order = PlannedOrder();
        _orderRepo.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync(order);
        _orderRepo.Setup(r => r.UpdateAsync(order, default)).Returns(Task.CompletedTask);

        await _handler.Handle(new CancelServiceOrderCommand(1), default);

        order.Status.Should().Be(ServiceOrderStatus.Cancelled);
    }

    [Fact]
    public async Task InProgressOrder_CanBeCancelled()
    {
        var order = PlannedOrder();
        order.Start();
        _orderRepo.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync(order);
        _orderRepo.Setup(r => r.UpdateAsync(order, default)).Returns(Task.CompletedTask);

        await _handler.Handle(new CancelServiceOrderCommand(1), default);

        order.Status.Should().Be(ServiceOrderStatus.Cancelled);
    }

    [Fact]
    public async Task CompletedOrder_CannotBeCancelled()
    {
        var order = PlannedOrder();
        order.Start();
        order.Complete();
        _orderRepo.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync(order);

        var act = () => _handler.Handle(new CancelServiceOrderCommand(1), default);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("*completed*");
    }

    [Fact]
    public async Task AlreadyCancelledOrder_CannotBeCancelledAgain()
    {
        var order = PlannedOrder();
        order.Cancel();
        _orderRepo.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync(order);

        var act = () => _handler.Handle(new CancelServiceOrderCommand(1), default);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("*cancelled*");
    }

    [Fact]
    public async Task OrderNotFound_ThrowsKeyNotFoundException()
    {
        _orderRepo.Setup(r => r.GetByIdAsync(99, default)).ReturnsAsync((ServiceOrder?)null);

        var act = () => _handler.Handle(new CancelServiceOrderCommand(99), default);

        await act.Should().ThrowAsync<KeyNotFoundException>();
    }
}
