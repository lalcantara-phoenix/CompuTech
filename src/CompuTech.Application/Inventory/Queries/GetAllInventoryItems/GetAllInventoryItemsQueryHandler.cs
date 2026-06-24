using CompuTech.Application.Inventory.DTOs;
using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.Inventory.Queries.GetAllInventoryItems;

public class GetAllInventoryItemsQueryHandler : IRequestHandler<GetAllInventoryItemsQuery, IReadOnlyList<InventoryItemDto>>
{
    private readonly IInventoryItemRepository _repository;

    public GetAllInventoryItemsQueryHandler(IInventoryItemRepository repository)
        => _repository = repository;

    public async Task<IReadOnlyList<InventoryItemDto>> Handle(GetAllInventoryItemsQuery request, CancellationToken ct)
    {
        var items = await _repository.GetAllAsync(ct);

        var filtered = items.AsEnumerable();

        if (request.Type is not null)
            filtered = filtered.Where(i => i.Type == request.Type);

        if (request.Category is not null)
            filtered = filtered.Where(i => i.Category == request.Category);

        if (request.InStockOnly == true)
            filtered = filtered.Where(i => i.StockQuantity > 0);

        return filtered
            .Select(item => new InventoryItemDto(
                item.Id,
                item.Name,
                item.SKU,
                item.Description,
                item.Type.ToString(),
                item.Category.ToString(),
                item.StockQuantity,
                item.SalePrice,
                item.IsActive,
                item.CreatedAt))
            .ToList()
            .AsReadOnly();
    }
}
