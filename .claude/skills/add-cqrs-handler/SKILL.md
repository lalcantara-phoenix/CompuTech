---
name: add-cqrs-handler
description: >
  Genera un Command o Query individual completo para el proyecto CompuTech:
  record de la operación, handler con IRequestHandler, y validator con
  AbstractValidator (solo para commands). Sigue las convenciones de nombre
  y estructura de Clean Architecture del proyecto.
argument-hint: "command|query [NombreOperacion]"
---

# Add CQRS Handler — CompuTech

Genera el handler CQRS indicado: `$ARGUMENTS`

El primer argumento es el tipo (`command` o `query`) y el segundo es el nombre de la operación en PascalCase (ej. `CreateCustomer`, `GetEquipmentById`).

## Para COMMAND (`command [NombreOperacion]`)

Genera tres archivos en `CompuTech.Application/[Modulo]/Commands/`:

### 1. `[NombreOperacion]Command.cs`
```csharp
public record [NombreOperacion]Command(
    // propiedades requeridas según el spec
) : IRequest<int>;  // retorna int (Id) o Unit si no crea recurso
```

### 2. `[NombreOperacion]CommandHandler.cs`
```csharp
public class [NombreOperacion]CommandHandler : IRequestHandler<[NombreOperacion]Command, int>
{
    private readonly I[Entidad]Repository _repository;

    public [NombreOperacion]CommandHandler(I[Entidad]Repository repository)
        => _repository = repository;

    public async Task<int> Handle([NombreOperacion]Command request, CancellationToken cancellationToken)
    {
        // lógica del caso de uso
        // aplicar reglas de negocio del spec si aplica
    }
}
```

### 3. `[NombreOperacion]CommandValidator.cs`
```csharp
public class [NombreOperacion]CommandValidator : AbstractValidator<[NombreOperacion]Command>
{
    public [NombreOperacion]CommandValidator()
    {
        // reglas basadas en el spec (NotEmpty, MaximumLength, etc.)
    }
}
```

---

## Para QUERY (`query [NombreOperacion]`)

Genera tres archivos:

### 1. `CompuTech.Application/[Modulo]/Queries/[NombreOperacion]Query.cs`
```csharp
public record [NombreOperacion]Query(
    // parámetros de búsqueda
) : IRequest<[Entidad]Dto?>;  // o IReadOnlyList<> si es lista
```

### 2. `CompuTech.Application/[Modulo]/Queries/[NombreOperacion]QueryHandler.cs`
```csharp
public class [NombreOperacion]QueryHandler : IRequestHandler<[NombreOperacion]Query, [Entidad]Dto?>
{
    private readonly I[Entidad]Repository _repository;

    public [NombreOperacion]QueryHandler(I[Entidad]Repository repository)
        => _repository = repository;

    public async Task<[Entidad]Dto?> Handle([NombreOperacion]Query request, CancellationToken cancellationToken)
    {
        // leer datos y mapear a DTO manualmente
    }
}
```

### 3. `CompuTech.Application/[Modulo]/DTOs/[Entidad]Dto.cs`
```csharp
public record [Entidad]Dto(
    // propiedades del DTO — nunca exponer entidades de dominio
);
```

---

## Pasos adicionales

1. Leer `docs/01-especificacion/especificacion.md` para conocer los campos y reglas de negocio exactos.
2. Leer los archivos existentes del módulo (si los hay) para no duplicar DTOs ya creados.
3. Inferir el nombre del módulo desde el nombre de la operación (ej. `CreateCustomer` → módulo `Customers`).
4. Si el command involucra descuento de stock (`ServiceOrderItem` de tipo `Part`), incluir la lógica de verificación y descuento.
5. Si el command completa una `ServiceOrder` de tipo `Preventive`, incluir la lógica de generación automática de la siguiente orden.
6. Actualizar `docs/BITACORA.md` con los archivos generados.

## Convenciones a respetar

- Commands retornan `int` (Id del recurso creado), `bool` (éxito de operación), o `Unit` (void).
- Queries retornan `[Entidad]Dto?` (por Id) o `IReadOnlyList<[Entidad]Dto>` (listas).
- Mapeo manual en handlers — no AutoMapper.
- `async/await` + `CancellationToken` en todos los métodos.
