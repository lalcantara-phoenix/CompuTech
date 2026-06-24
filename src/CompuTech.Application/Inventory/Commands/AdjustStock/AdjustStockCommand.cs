using MediatR;

namespace CompuTech.Application.Inventory.Commands.AdjustStock;

public record AdjustStockCommand(int Id, int Delta) : IRequest;
