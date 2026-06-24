using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.Equipment.Commands.DeactivateEquipment;

public class DeactivateEquipmentCommandHandler(IEquipmentRepository repository)
    : IRequestHandler<DeactivateEquipmentCommand>
{
    public async Task Handle(DeactivateEquipmentCommand request, CancellationToken ct)
    {
        var equipment = await repository.GetByIdAsync(request.Id, ct)
            ?? throw new InvalidOperationException("Equipment not found");

        equipment.Deactivate();
        await repository.UpdateAsync(equipment, ct);
    }
}
