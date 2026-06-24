using CompuTech.Application.ServiceOrders.Commands.AddServiceOrderItem;
using CompuTech.Domain.Entities;
using CompuTech.Domain.Enums;
using CompuTech.Domain.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace CompuTech.Application.Tests.ServiceOrders.Handlers;

public class AddServiceOrderItemCommandHandlerTests
{
    private readonly Mock<IServiceOrderRepository> _orderRepo = new();
    private readonly Mock<IServiceOrderItemRepository> _itemRepo = new();
    private readonly Mock<IInventoryItemRepository> _inventoryRepo = new();
    private readonly AddServiceOrderItemCommandHandler _handler;

    public AddServiceOrderItemCommandHandlerTests()
    {
        _handler = new AddServiceOrderItemCommandHandler(
            _orderRepo.Object, _itemRepo.Object, _inventoryRepo.Object);
    }

    private static ServiceOrder PlannedOrder() =>
        ServiceOrder.Create("ORD-2026-0001", 1, 1,
            ServiceOrderType.Repair, ServiceOrderSubType.Hardware,
            ServiceOrderPriority.Medium, "Test order");

    private static InventoryItem ActiveInventoryItem(int stock = 10) =>
        InventoryItem.Create("RAM 8GB", "RAM-001", null,
            InventoryItemType.Part, InventoryCategory.Memory,
            stock, 50m);

    [Fact]
    public async Task Rule6_Subtotal_EqualsQuantityTimesUnitPrice()
    {
        var item = ServiceOrderItem.Create(1, ServiceOrderItemType.Labor, "Tech hour", 3, 75m);

        item.Subtotal.Should().Be(225m);
    }

    [Fact]
    public async Task Part_DecreasesInventoryStock()
    {
        var order = PlannedOrder();
        var inventoryItem = ActiveInventoryItem(10);

        _orderRepo.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync(order);
        _inventoryRepo.Setup(r => r.GetByIdAsync(10, default)).ReturnsAsync(inventoryItem);
        _inventoryRepo.Setup(r => r.UpdateAsync(inventoryItem, default)).Returns(Task.CompletedTask);
        _itemRepo.Setup(r => r.AddAsync(It.IsAny<ServiceOrderItem>(), default)).Returns(Task.CompletedTask);

        await _handler.Handle(new AddServiceOrderItemCommand(1, "Part", "RAM 8GB", 3, 50m, 10), default);

        inventoryItem.StockQuantity.Should().Be(7);
    }

    [Fact]
    public async Task Labor_DoesNotTouchInventory()
    {
        var order = PlannedOrder();
        _orderRepo.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync(order);
        _itemRepo.Setup(r => r.AddAsync(It.IsAny<ServiceOrderItem>(), default)).Returns(Task.CompletedTask);

        await _handler.Handle(new AddServiceOrderItemCommand(1, "Labor", "Technician hour", 2, 75m), default);

        _inventoryRepo.Verify(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
        _inventoryRepo.Verify(r => r.UpdateAsync(It.IsAny<InventoryItem>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task CompletedOrder_CannotAddItems_ThrowsInvalidOperationException()
    {
        var order = PlannedOrder();
        order.Start();
        order.Complete();

        _orderRepo.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync(order);

        var act = () => _handler.Handle(new AddServiceOrderItemCommand(1, "Labor", "Tech hour", 1, 75m), default);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("*completed or cancelled*");
    }

    [Fact]
    public async Task OrderNotFound_ThrowsKeyNotFoundException()
    {
        _orderRepo.Setup(r => r.GetByIdAsync(99, default)).ReturnsAsync((ServiceOrder?)null);

        var act = () => _handler.Handle(new AddServiceOrderItemCommand(99, "Labor", "Tech hour", 1, 75m), default);

        await act.Should().ThrowAsync<KeyNotFoundException>();
    }
}
