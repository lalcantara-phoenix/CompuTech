using CompuTech.Domain.Enums;
using MediatR;

namespace CompuTech.Application.Equipment.Commands.UpdateEquipment;

public record UpdateEquipmentCommand(int Id, string Brand, string Model, EquipmentType Type) : IRequest;
