using MediatR;

namespace CompuTech.Application.Equipment.Commands.DeactivateEquipment;

public record DeactivateEquipmentCommand(int Id) : IRequest;
