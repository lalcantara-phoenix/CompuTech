using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.Equipment.Commands.UpdateEquipment;

public class UpdateEquipmentCommandHandler(IEquipmentRepository repository)
    : IRequestHandler<UpdateEquipmentCommand>
{
    public async Task Handle(UpdateEquipmentCommand request, CancellationToken ct)
    {
        var equipment = await repository.GetByIdAsync(request.Id, ct)
            ?? throw new InvalidOperationException("Equipment not found");

        equipment.Update(request.Brand, request.Model, request.Type);
        await repository.UpdateAsync(equipment, ct);
    }
}
