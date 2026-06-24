# Evidencia 3 — Test-driven Iteration

**Criterio:** Documentar al menos un ciclo TDD: escribir prueba con Claude → fallar → implementar → pasar. Incluir en el archivo de evidencias.

---

## Descripción

El ciclo TDD se aplicó durante la implementación de las **reglas de negocio críticas** del módulo ServiceOrders. Las pruebas fueron diseñadas con el skill `/design-tests` (agente `dotnet-unit-test`) antes de completar la implementación de los handlers.

---

## Ciclo TDD Documentado: Regla #8

**Regla #8:** Al completar una orden de mantenimiento preventivo, el sistema debe generar automáticamente la siguiente orden de servicio.

### Paso 1 — Escribir la prueba (RED)

El agente `dotnet-unit-test` generó el test antes de que `CompleteServiceOrderCommandHandler` implementara la lógica de la regla #8:

**Archivo:** `tests/CompuTech.Application.Tests/ServiceOrders/Handlers/CompleteServiceOrderCommandHandlerTests.cs`

```csharp
[Fact]
public async Task Rule8_PreventiveOrder_CallsGenerateNextMaintenanceOrder()
{
    var order = ServiceOrder.Create(
        "ORD-2026-0001", 1, 1,
        ServiceOrderType.Maintenance, ServiceOrderSubType.Preventive,
        ServiceOrderPriority.Medium, "Preventive maintenance");
    order.Start();

    _orderRepo.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync(order);
    _orderRepo.Setup(r => r.UpdateAsync(order, default)).Returns(Task.CompletedTask);
    _generateService.Setup(s => s.GenerateAsync(order, default)).Returns(Task.CompletedTask);

    await _handler.Handle(new CompleteServiceOrderCommand(1), default);

    // Verifica que el servicio fue llamado exactamente una vez
    _generateService.Verify(s => s.GenerateAsync(order, default), Times.Once);
}

[Fact]
public async Task Rule8_RepairOrder_DoesNotCallGenerateNextMaintenanceOrder()
{
    var order = CreateInProgressOrder(ServiceOrderSubType.Hardware);
    _orderRepo.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync(order);
    _orderRepo.Setup(r => r.UpdateAsync(order, default)).Returns(Task.CompletedTask);

    await _handler.Handle(new CompleteServiceOrderCommand(1), default);

    // Verifica que NO se llamó al servicio para una orden de reparación
    _generateService.Verify(
        s => s.GenerateAsync(It.IsAny<ServiceOrder>(), It.IsAny<CancellationToken>()),
        Times.Never);
}
```

### Paso 2 — Implementar (GREEN)

El handler fue implementado con la lógica de la regla #8:

**Archivo:** `src/CompuTech.Application/ServiceOrders/Commands/CompleteServiceOrder/CompleteServiceOrderCommandHandler.cs`

```csharp
public async Task Handle(CompleteServiceOrderCommand request, CancellationToken cancellationToken)
{
    var order = await serviceOrderRepository.GetByIdAsync(request.Id, cancellationToken)
        ?? throw new KeyNotFoundException($"ServiceOrder {request.Id} not found");

    if (order.Status != ServiceOrderStatus.InProgress)
        throw new InvalidOperationException(
            $"Only InProgress orders can be completed. Current status: {order.Status}");

    order.Complete(request.ResolutionNotes);
    await serviceOrderRepository.UpdateAsync(order, cancellationToken);

    // Regla #8: auto-generate next order for Preventive maintenance
    if (order.SubType == ServiceOrderSubType.Preventive)
        await generateNextMaintenanceOrderService.GenerateAsync(order, cancellationToken);
}
```

### Paso 3 — Verificar (PASS)

```
dotnet test tests/CompuTech.Application.Tests/CompuTech.Application.Tests.csproj
  --filter "FullyQualifiedName~CompleteServiceOrderCommandHandlerTests"

Test Run Summary:
  Total: 5  |  Passed: 5  |  Failed: 0  |  Duration: 1.2s
```

---

## Ciclo TDD Adicional: Regla #5

**Regla #5:** El SubType debe ser compatible con el Type (Repair↔Hardware/Software/Diagnostic; Maintenance↔Preventive/OnDemand).

### Test (RED → PASS)

**Archivo:** `tests/CompuTech.Application.Tests/ServiceOrders/Validators/CreateServiceOrderCommandValidatorTests.cs`

```csharp
[Theory]
[InlineData("Repair", "Preventive")]    // inválido
[InlineData("Repair", "OnDemand")]      // inválido
[InlineData("Maintenance", "Hardware")] // inválido
public void Rule5_InvalidTypeSubTypeCombinations_ShouldFail(string type, string subType)
{
    var result = _validator.TestValidate(ValidCommand(type, subType));

    result.ShouldHaveValidationErrorFor(x => x)
        .WithErrorMessage("SubType is not valid for the specified Type");
}

[Theory]
[InlineData("Repair", "Hardware")]      // válido
[InlineData("Repair", "Software")]      // válido
[InlineData("Maintenance", "Preventive")] // válido
public void Rule5_ValidTypeSubTypeCombinations_ShouldPass(string type, string subType)
{
    var result = _validator.TestValidate(ValidCommand(type, subType));

    result.ShouldNotHaveAnyValidationErrors();
}
```

---

## Resultado Final

```
dotnet test tests/CompuTech.Application.Tests/CompuTech.Application.Tests.csproj

Test Run Summary:
  Total: 44  |  Passed: 44  |  Failed: 0  |  Skipped: 0
  Duration: 3.8s
```

**44 pruebas — 0 fallos.**

## Suite Completa de Tests

| Archivo | Tests | Reglas Cubiertas |
|---|---|---|
| `CompleteServiceOrderCommandHandlerTests.cs` | 5 | #8 |
| `CancelServiceOrderCommandHandlerTests.cs` | 4 | Transiciones de estado |
| `AddServiceOrderItemCommandHandlerTests.cs` | 6 | #3, #6 |
| `CreateServiceOrderCommandValidatorTests.cs` | 7 | #5 |
| `AddServiceOrderItemCommandValidatorTests.cs` | 6 | #3, #4 |
| `CreateMaintenanceScheduleCommandHandlerTests.cs` | 8 | #1 |
| `GenerateNextMaintenanceOrderServiceTests.cs` | 8 | #8 |

## Commit de Evidencia

```
73492cd  [Tests] Suite de pruebas unitarias — 44 tests cubriendo reglas #1, #3, #4, #5, #6, #8
22f443e  release(tests): suite de pruebas unitarias — 44 tests, 0 fallos
```
