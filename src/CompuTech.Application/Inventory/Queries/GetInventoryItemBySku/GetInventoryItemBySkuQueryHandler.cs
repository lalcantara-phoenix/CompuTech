using CompuTech.Application.Inventory.DTOs;
using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.Inventory.Queries.GetInventoryItemBySku;

public class GetInventoryItemBySkuQueryHandler : IRequestHandler<GetInventoryItemBySkuQuery, InventoryItemDto?>
{
    private readonly IInventoryItemRepository _repository;

    public GetInventoryItemBySkuQueryHandler(IInventoryItemRepository repository)
        => _repository = repository;

    public async Task<InventoryItemDto?> Handle(GetInventoryItemBySkuQuery request, CancellationToken ct)
    {
        var item = await _repository.GetBySkuAsync(request.SKU, ct);

        if (item is null)
            return null;

        return new InventoryItemDto(
            item.Id,
            item.Name,
            item.SKU,
            item.Description,
            item.Type.ToString(),
            item.Category.ToString(),
            item.StockQuantity,
            item.SalePrice,
            item.IsActive,
            item.CreatedAt);
    }
}
