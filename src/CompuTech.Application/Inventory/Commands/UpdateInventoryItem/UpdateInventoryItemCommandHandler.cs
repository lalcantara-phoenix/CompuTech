using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.Inventory.Commands.UpdateInventoryItem;

public class UpdateInventoryItemCommandHandler(IInventoryItemRepository repository)
    : IRequestHandler<UpdateInventoryItemCommand>
{
    public async Task Handle(UpdateInventoryItemCommand request, CancellationToken ct)
    {
        var item = await repository.GetByIdAsync(request.Id, ct)
            ?? throw new InvalidOperationException($"InventoryItem with ID {request.Id} not found");

        item.Update(request.Name, request.Description, request.Type, request.Category, request.SalePrice);
        await repository.UpdateAsync(item, ct);
    }
}
