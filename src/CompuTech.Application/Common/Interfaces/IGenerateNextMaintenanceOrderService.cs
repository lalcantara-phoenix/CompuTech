using CompuTech.Domain.Entities;

namespace CompuTech.Application.Common.Interfaces;

public interface IGenerateNextMaintenanceOrderService
{
    Task GenerateAsync(ServiceOrder completedOrder, CancellationToken ct = default);
}
