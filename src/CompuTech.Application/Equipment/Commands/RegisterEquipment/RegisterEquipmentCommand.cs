using CompuTech.Domain.Enums;
using MediatR;

namespace CompuTech.Application.Equipment.Commands.RegisterEquipment;

public record RegisterEquipmentCommand(
    int CustomerId,
    int LocationId,
    string SerialNumber,
    string Brand,
    string Model,
    EquipmentType Type,
    string? OperatingSystem,
    DateTime? PurchaseDate,
    string? Notes
) : IRequest<int>;
