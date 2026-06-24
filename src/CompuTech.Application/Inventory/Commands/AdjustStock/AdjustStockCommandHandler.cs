using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.Inventory.Commands.AdjustStock;

public class AdjustStockCommandHandler(IInventoryItemRepository repository)
    : IRequestHandler<AdjustStockCommand>
{
    public async Task Handle(AdjustStockCommand request, CancellationToken ct)
    {
        var item = await repository.GetByIdAsync(request.Id, ct)
            ?? throw new InvalidOperationException($"InventoryItem with ID {request.Id} not found");

        item.AdjustStock(request.Delta);
        await repository.UpdateAsync(item, ct);
    }
}
