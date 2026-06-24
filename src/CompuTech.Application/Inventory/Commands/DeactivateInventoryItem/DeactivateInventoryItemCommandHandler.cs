using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.Inventory.Commands.DeactivateInventoryItem;

public class DeactivateInventoryItemCommandHandler(IInventoryItemRepository repository)
    : IRequestHandler<DeactivateInventoryItemCommand>
{
    public async Task Handle(DeactivateInventoryItemCommand request, CancellationToken ct)
    {
        var item = await repository.GetByIdAsync(request.Id, ct)
            ?? throw new InvalidOperationException($"InventoryItem with ID {request.Id} not found");

        item.Deactivate();
        await repository.UpdateAsync(item, ct);
    }
}
