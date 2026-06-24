namespace CompuTech.API.Models.ServiceOrders;

public record CreateServiceOrderRequest(
    int EquipmentId,
    int TechnicianId,
    string Type,
    string SubType,
    string Priority,
    string Description,
    DateTime? ScheduledAt = null,
    int? ScheduleId = null
);

public record UpdateServiceOrderRequest(
    int TechnicianId,
    string Priority,
    string Description,
    DateTime? ScheduledAt = null
);

public record CompleteServiceOrderRequest(string? ResolutionNotes = null);

public record AddServiceOrderItemRequest(
    string ItemType,
    string Description,
    int Quantity,
    decimal UnitPrice,
    int? InventoryItemId = null,
    string? Notes = null
);
