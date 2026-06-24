---
name: design-tests
description: >
  Diseña y genera la suite completa de pruebas unitarias para un módulo del proyecto CompuTech.
  Crea tests para handlers CQRS, validadores FluentValidation y reglas de negocio usando
  xUnit, Moq y FluentAssertions. Registra el plan de cobertura en docs/05-pruebas/.
argument-hint: "[NombreModulo]"
---

# Design Tests — CompuTech

Diseña y genera la suite de pruebas unitarias para el módulo: `$ARGUMENTS`

---

## Pasos a ejecutar

### 1. Análisis del módulo

Leer todos los archivos del módulo en `CompuTech.Application/$ARGUMENTS/`:
- Commands y sus handlers
- Queries y sus handlers
- Validators
- DTOs

Identificar para cada handler:
- El happy path (flujo exitoso)
- Los casos de error esperados
- Las reglas de negocio del spec que aplican

### 2. Crear Test Data Builder

Si no existe, crear `CompuTech.Application.Tests/Common/Builders/$ARGUMENTSBuilder.cs`:

```csharp
public class $ARGUMENTSBuilder
{
    // campos con valores por defecto válidos
    public $ARGUMENTSBuilder With[Campo]([Tipo] value) { ... return this; }
    public [Entidad] Build() => [Entidad].Create(...);
}
```

### 3. Generar tests de Commands

Para cada command handler, crear `CompuTech.Application.Tests/$ARGUMENTS/Commands/[Nombre]CommandHandlerTests.cs`:

- Happy path: comando válido → recurso creado/modificado → Id retornado
- Recurso no encontrado (para updates/deletes): → `NotFoundException`
- Reglas de negocio específicas del módulo

### 4. Generar tests de Queries

Para cada query handler, crear `CompuTech.Application.Tests/$ARGUMENTS/Queries/[Nombre]QueryHandlerTests.cs`:

- Happy path: recurso existe → DTO retornado con campos correctos
- Recurso no encontrado → `null` retornado
- Lista vacía → `IReadOnlyList` vacía (no null)

### 5. Generar tests de Validators

Para cada validator, tests de:
- Comando completamente válido → `IsValid = true`
- Cada campo requerido vacío → error de validación en ese campo
- Campos que exceden longitud máxima → error de validación
- Casos de formato inválido (emails, etc.)

### 6. Tests de reglas de negocio especiales

Según el módulo `$ARGUMENTS`, incluir:

**Si es ServiceOrders:**
- `Handle_WhenSubTypeIncompatibleWithType_ThrowsDomainException`
- `Handle_WhenStatusTransitionInvalid_ThrowsDomainException`
- `Handle_WhenPreventiveOrderCompleted_GeneratesNextServiceOrder`
- `Handle_WhenScheduleInactive_DoesNotGenerateNextOrder`

**Si es ServiceOrderItems:**
- `Handle_WhenItemTypeIsPartAndInventoryItemIdIsNull_ThrowsValidationException`
- `Handle_WhenStockInsufficient_ThrowsDomainException`
- `Handle_WhenPartAdded_DecrementsInventoryStock`
- `Handle_WhenItemTypeIsLabor_InventoryItemIdMustBeNull`

**Si es Equipment:**
- `Handle_WhenEquipmentAlreadyHasActiveSchedule_ThrowsDomainException`

### 7. Documentar el plan de cobertura

Crear `docs/05-pruebas/$ARGUMENTS-tests.md` con:
- Número total de tests generados
- Tests por categoría (commands, queries, validators, reglas de negocio)
- Estimación de cobertura de ramas (%)
- Casos límite no cubiertos (si los hay)

### 8. Actualizar la bitácora

Actualizar `docs/BITACORA.md` con los tests generados.

---

## Convenciones

- Nombre de método: `Handle_[Escenario]_[ResultadoEsperado]`
- Un `[Fact]` por comportamiento observable
- `[Theory] + [InlineData]` solo para variaciones de datos con lógica idéntica
- Usar `Mock<I[Entidad]Repository>` — nunca tocar la base de datos real
- FluentAssertions para todas las aserciones (`result.Should().Be(...)`)
- No testear métodos privados ni detalles de implementación
