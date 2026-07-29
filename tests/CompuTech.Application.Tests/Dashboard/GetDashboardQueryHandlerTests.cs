using CompuTech.Application.Dashboard.Queries.GetDashboard;
using CompuTech.Domain.Enums;
using CompuTech.Domain.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;
using InventoryItemEntity = CompuTech.Domain.Entities.InventoryItem;
using MaintenanceScheduleEntity = CompuTech.Domain.Entities.MaintenanceSchedule;

namespace CompuTech.Application.Tests.Dashboard;

public class GetDashboardQueryHandlerTests
{
    private readonly Mock<IServiceOrderRepository> _serviceOrderRepo = new();
    private readonly Mock<ITechnicianRepository> _technicianRepo = new();
    private readonly Mock<IInventoryItemRepository> _inventoryItemRepo = new();
    private readonly Mock<IMaintenanceScheduleRepository> _maintenanceScheduleRepo = new();
    private readonly GetDashboardQueryHandler _handler;

    public GetDashboardQueryHandlerTests()
    {
        _handler = new GetDashboardQueryHandler(
            _serviceOrderRepo.Object,
            _technicianRepo.Object,
            _inventoryItemRepo.Object,
            _maintenanceScheduleRepo.Object);
    }

    /// <summary>
    /// Configures all four repository mocks in one call.
    /// Each parameter is optional; omitting it causes that repo to return an empty collection.
    /// </summary>
    private void SetupAllMocks(
        IReadOnlyList<(ServiceOrderStatus Status, int Count)>? statusCounts = null,
        IReadOnlyList<(int TechnicianId, string FullName, int ActiveOrderCount)>? technicians = null,
        IReadOnlyList<InventoryItemEntity>? inventoryItems = null,
        IReadOnlyList<MaintenanceScheduleEntity>? schedules = null)
    {
        _serviceOrderRepo
            .Setup(r => r.GetCountsByStatusAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(statusCounts ?? Array.Empty<(ServiceOrderStatus, int)>());

        _technicianRepo
            .Setup(r => r.GetTopByActiveOrdersAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(technicians ?? Array.Empty<(int, string, int)>());

        _inventoryItemRepo
            .Setup(r => r.GetBelowStockThresholdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(inventoryItems ?? Array.Empty<InventoryItemEntity>());

        _maintenanceScheduleRepo
            .Setup(r => r.GetDueAsync(It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(schedules ?? Array.Empty<MaintenanceScheduleEntity>());
    }

    // -------------------------------------------------------------------------
    // Test 1 — Happy path: all sections populated
    // -------------------------------------------------------------------------

    [Fact]
    public async Task Handle_ReturnsAllSections_WhenAllRepositoriesHaveData()
    {
        // Arrange
        var statusCounts = new List<(ServiceOrderStatus Status, int Count)>
        {
            (ServiceOrderStatus.Planned, 3),
            (ServiceOrderStatus.InProgress, 2),
            (ServiceOrderStatus.Completed, 10)
        };

        var technicians = new List<(int TechnicianId, string FullName, int ActiveOrderCount)>
        {
            (1, "Juan Pérez", 5),
            (2, "María López", 3)
        };

        var inventoryItems = new List<InventoryItemEntity>
        {
            InventoryItemEntity.Create(
                "RAM 8GB", "RAM-001", null,
                InventoryItemType.Part, InventoryCategory.Memory,
                initialStock: 2, salePrice: 45.00m),
            InventoryItemEntity.Create(
                "SSD 512GB", "SSD-001", null,
                InventoryItemType.Part, InventoryCategory.Storage,
                initialStock: 1, salePrice: 89.99m)
        };

        var nextDate = DateTime.UtcNow.AddDays(10);
        var schedules = new List<MaintenanceScheduleEntity>
        {
            MaintenanceScheduleEntity.Create(1, MaintenanceFrequency.Monthly, nextDate),
            MaintenanceScheduleEntity.Create(2, MaintenanceFrequency.Quarterly, nextDate.AddDays(5))
        };

        SetupAllMocks(statusCounts, technicians, inventoryItems, schedules);

        // Act
        var result = await _handler.Handle(new GetDashboardQuery(), default);

        // Assert
        result.Should().NotBeNull();
        result.OrdersByStatus.Should().NotBeEmpty();
        result.TopTechnicians.Should().HaveCount(2);
        result.LowStockItems.Should().HaveCount(2);
        result.DueMaintenanceSchedules.Should().HaveCount(2);
    }

    // -------------------------------------------------------------------------
    // Test 2 — All repos empty: no exception; sections empty except OrdersByStatus
    // -------------------------------------------------------------------------

    [Fact]
    public async Task Handle_ReturnsEmptySections_WhenRepositoriesReturnEmpty()
    {
        // Arrange
        SetupAllMocks(); // all defaults → empty collections

        // Act
        var act = async () => await _handler.Handle(new GetDashboardQuery(), default);
        var result = await _handler.Handle(new GetDashboardQuery(), default);

        // Assert — no exception
        await act.Should().NotThrowAsync();

        result.TopTechnicians.Should().BeEmpty();
        result.LowStockItems.Should().BeEmpty();
        result.DueMaintenanceSchedules.Should().BeEmpty();

        // OrdersByStatus always has one entry per enum value, even when counts are 0
        result.OrdersByStatus.Should().NotBeEmpty(
            because: "the handler enumerates all ServiceOrderStatus values regardless of repo data");
    }

    // -------------------------------------------------------------------------
    // Test 3 — OrdersByStatus covers every ServiceOrderStatus value
    // -------------------------------------------------------------------------

    [Fact]
    public async Task Handle_OrdersByStatus_IncludesAllStatusValues()
    {
        // Arrange
        SetupAllMocks();
        var allStatuses = Enum.GetValues<ServiceOrderStatus>();

        // Act
        var result = await _handler.Handle(new GetDashboardQuery(), default);

        // Assert
        result.OrdersByStatus.Should().HaveCount(allStatuses.Length,
            because: "every ServiceOrderStatus value must appear exactly once");

        foreach (var status in allStatuses)
        {
            result.OrdersByStatus
                .Should().Contain(s => s.Status == status.ToString(),
                    because: $"'{status}' must be present in OrdersByStatus");
        }
    }

    // -------------------------------------------------------------------------
    // Test 4 — Counts coming from the repo are mapped to the correct status
    // -------------------------------------------------------------------------

    [Fact]
    public async Task Handle_OrdersByStatus_MapsCountsCorrectly()
    {
        // Arrange
        var statusCounts = new List<(ServiceOrderStatus Status, int Count)>
        {
            (ServiceOrderStatus.Planned, 10),
            (ServiceOrderStatus.InProgress, 4),
            (ServiceOrderStatus.Completed, 25)
            // Delayed and Cancelled intentionally absent → expected Count = 0
        };
        SetupAllMocks(statusCounts: statusCounts);

        // Act
        var result = await _handler.Handle(new GetDashboardQuery(), default);

        // Assert — statuses present in the repo data are mapped correctly
        result.OrdersByStatus
            .First(s => s.Status == nameof(ServiceOrderStatus.Planned)).Count
            .Should().Be(10);

        result.OrdersByStatus
            .First(s => s.Status == nameof(ServiceOrderStatus.InProgress)).Count
            .Should().Be(4);

        result.OrdersByStatus
            .First(s => s.Status == nameof(ServiceOrderStatus.Completed)).Count
            .Should().Be(25);

        // Assert — statuses absent in the repo data default to 0
        result.OrdersByStatus
            .First(s => s.Status == nameof(ServiceOrderStatus.Delayed)).Count
            .Should().Be(0);

        result.OrdersByStatus
            .First(s => s.Status == nameof(ServiceOrderStatus.Cancelled)).Count
            .Should().Be(0);
    }

    // -------------------------------------------------------------------------
    // Test 5 — Handler passes the hard-coded top-5 constant to the repo
    // -------------------------------------------------------------------------

    [Fact]
    public async Task Handle_TopTechnicians_PassesCorrectTopCount()
    {
        // Arrange
        SetupAllMocks();

        // Act
        await _handler.Handle(new GetDashboardQuery(), default);

        // Assert
        _technicianRepo.Verify(
            r => r.GetTopByActiveOrdersAsync(5, It.IsAny<CancellationToken>()),
            Times.Once,
            "the handler must request exactly the top-5 technicians");
    }

    // -------------------------------------------------------------------------
    // Test 6 — Handler passes the hard-coded threshold-5 constant to the repo
    // -------------------------------------------------------------------------

    [Fact]
    public async Task Handle_LowStockItems_PassesCorrectThreshold()
    {
        // Arrange
        SetupAllMocks();

        // Act
        await _handler.Handle(new GetDashboardQuery(), default);

        // Assert
        _inventoryItemRepo.Verify(
            r => r.GetBelowStockThresholdAsync(5, It.IsAny<CancellationToken>()),
            Times.Once,
            "the handler must use threshold = 5 when querying low-stock items");
    }

    // -------------------------------------------------------------------------
    // Test 7 — Mapping: TechnicianWorkloadDto fields come from the repo tuple
    // -------------------------------------------------------------------------

    [Fact]
    public async Task Handle_TopTechnicians_MapsTupleFieldsToDto()
    {
        // Arrange
        var technicians = new List<(int TechnicianId, string FullName, int ActiveOrderCount)>
        {
            (42, "Carlos Martínez", 7)
        };
        SetupAllMocks(technicians: technicians);

        // Act
        var result = await _handler.Handle(new GetDashboardQuery(), default);

        // Assert
        var dto = result.TopTechnicians.Should().ContainSingle().Subject;
        dto.TechnicianId.Should().Be(42);
        dto.FullName.Should().Be("Carlos Martínez");
        dto.ActiveOrderCount.Should().Be(7);
    }

    // -------------------------------------------------------------------------
    // Test 8 — Mapping: DueScheduleDto fields come from MaintenanceSchedule entity
    // -------------------------------------------------------------------------

    [Fact]
    public async Task Handle_DueMaintenanceSchedules_MapsEntityFieldsToDto()
    {
        // Arrange
        var nextService = new DateTime(2026, 8, 15, 0, 0, 0, DateTimeKind.Utc);
        var schedule = MaintenanceScheduleEntity.Create(
            equipmentId: 10,
            frequency: MaintenanceFrequency.SemiAnnual,
            nextServiceDate: nextService);

        SetupAllMocks(schedules: new List<MaintenanceScheduleEntity> { schedule });

        // Act
        var result = await _handler.Handle(new GetDashboardQuery(), default);

        // Assert
        var dto = result.DueMaintenanceSchedules.Should().ContainSingle().Subject;
        dto.EquipmentId.Should().Be(10);
        dto.Frequency.Should().Be(nameof(MaintenanceFrequency.SemiAnnual));
        dto.NextServiceDate.Should().Be(nextService);
    }

    // -------------------------------------------------------------------------
    // Test 9 — Mapping: LowStockItemDto fields come from InventoryItem entity
    // -------------------------------------------------------------------------

    [Fact]
    public async Task Handle_LowStockItems_MapsEntityFieldsToDto()
    {
        // Arrange
        var item = InventoryItemEntity.Create(
            name: "Pasta Térmica",
            sku: "PASTE-001",
            description: null,
            type: InventoryItemType.Part,
            category: InventoryCategory.CoolingSystem,
            initialStock: 3,
            salePrice: 5.50m);

        SetupAllMocks(inventoryItems: new List<InventoryItemEntity> { item });

        // Act
        var result = await _handler.Handle(new GetDashboardQuery(), default);

        // Assert
        var dto = result.LowStockItems.Should().ContainSingle().Subject;
        dto.Name.Should().Be("Pasta Térmica");
        dto.Sku.Should().Be("PASTE-001");
        dto.Stock.Should().Be(3);
    }
}
