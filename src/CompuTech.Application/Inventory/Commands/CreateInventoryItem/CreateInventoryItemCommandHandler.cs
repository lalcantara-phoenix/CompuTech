using CompuTech.Domain.Interfaces;
using MediatR;
using InventoryItemEntity = CompuTech.Domain.Entities.InventoryItem;

namespace CompuTech.Application.Inventory.Commands.CreateInventoryItem;

public class CreateInventoryItemCommandHandler(IInventoryItemRepository repository)
    : IRequestHandler<CreateInventoryItemCommand, int>
{
    public async Task<int> Handle(CreateInventoryItemCommand request, CancellationToken ct)
    {
        var existing = await repository.GetBySkuAsync(request.SKU, ct);
        if (existing is not null)
            throw new InvalidOperationException("SKU already registered");

        var item = InventoryItemEntity.Create(
            request.Name,
            request.SKU,
            request.Description,
            request.Type,
            request.Category,
            request.InitialStock,
            request.SalePrice);

        await repository.AddAsync(item, ct);
        return item.Id;
    }
}
