using CompuTech.Domain.Enums;

namespace CompuTech.Domain.Entities;

public class ServiceOrder
{
    public int Id { get; private set; }
    public string OrderNumber { get; private set; } = default!;
    public int EquipmentId { get; private set; }
    public int TechnicianId { get; private set; }
    public int? ScheduleId { get; private set; }
    public ServiceOrderType Type { get; private set; }
    public ServiceOrderSubType SubType { get; private set; }
    public ServiceOrderStatus Status { get; private set; }
    public ServiceOrderPriority Priority { get; private set; }
    public string Description { get; private set; } = default!;
    public string? DiagnosisNotes { get; private set; }
    public string? ResolutionNotes { get; private set; }
    public DateTime? ScheduledAt { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private List<ServiceOrderItem> _items = new();
    public IReadOnlyList<ServiceOrderItem> Items => _items.AsReadOnly();

    private ServiceOrder() { }

    public static string GenerateOrderNumber(int year, int sequence) => $"ORD-{year}-{sequence:D4}";

    public static ServiceOrder Create(
        string orderNumber,
        int equipmentId,
        int technicianId,
        ServiceOrderType type,
        ServiceOrderSubType subType,
        ServiceOrderPriority priority,
        string description,
        DateTime? scheduledAt = null,
        int? scheduleId = null)
    {
        return new ServiceOrder
        {
            OrderNumber = orderNumber,
            EquipmentId = equipmentId,
            TechnicianId = technicianId,
            Type = type,
            SubType = subType,
            Status = ServiceOrderStatus.Planned,
            Priority = priority,
            Description = description,
            ScheduledAt = scheduledAt,
            ScheduleId = scheduleId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Start()
    {
        Status = ServiceOrderStatus.InProgress;
        StartedAt = DateTime.UtcNow;
    }

    public void Complete(string? resolutionNotes = null)
    {
        Status = ServiceOrderStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        ResolutionNotes = resolutionNotes;
    }

    public void Delay() => Status = ServiceOrderStatus.Delayed;

    public void Cancel() => Status = ServiceOrderStatus.Cancelled;

    public void Update(int technicianId, ServiceOrderPriority priority, string description, DateTime? scheduledAt)
    {
        TechnicianId = technicianId;
        Priority = priority;
        Description = description;
        ScheduledAt = scheduledAt;
    }

    public void SetDiagnosisNotes(string notes) => DiagnosisNotes = notes;

    public void AddItem(ServiceOrderItem item) => _items.Add(item);

    public void RemoveItem(ServiceOrderItem item) => _items.Remove(item);
}
