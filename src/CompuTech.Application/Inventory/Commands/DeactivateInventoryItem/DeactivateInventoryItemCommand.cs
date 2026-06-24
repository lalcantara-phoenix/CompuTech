using MediatR;

namespace CompuTech.Application.Inventory.Commands.DeactivateInventoryItem;

public record DeactivateInventoryItemCommand(int Id) : IRequest;
