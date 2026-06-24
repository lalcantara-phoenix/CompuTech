namespace CompuTech.Application.ServiceOrders.DTOs;

public record ServiceOrderDto(
    int Id,
    string OrderNumber,
    int EquipmentId,
    int TechnicianId,
    int? ScheduleId,
    string Type,
    string SubType,
    string Status,
    string Priority,
    string Description,
    string? DiagnosisNotes,
    string? ResolutionNotes,
    DateTime? ScheduledAt,
    DateTime? StartedAt,
    DateTime? CompletedAt,
    DateTime CreatedAt
);
