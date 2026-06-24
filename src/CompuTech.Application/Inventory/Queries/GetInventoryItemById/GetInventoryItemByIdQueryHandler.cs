using CompuTech.Application.Inventory.DTOs;
using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.Inventory.Queries.GetInventoryItemById;

public class GetInventoryItemByIdQueryHandler : IRequestHandler<GetInventoryItemByIdQuery, InventoryItemDto?>
{
    private readonly IInventoryItemRepository _repository;

    public GetInventoryItemByIdQueryHandler(IInventoryItemRepository repository)
        => _repository = repository;

    public async Task<InventoryItemDto?> Handle(GetInventoryItemByIdQuery request, CancellationToken ct)
    {
        var item = await _repository.GetByIdAsync(request.Id, ct);

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
