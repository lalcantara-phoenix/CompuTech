# Evidencia 5 — Security

**Criterio:** Solicitar a Claude una revisión de seguridad del endpoint más crítico e incluir los hallazgos o confirmaciones en las evidencias.

---

## Endpoint Revisado

**`POST /api/service-orders/{id}/complete`** — Endpoint más crítico del sistema. Dispara la regla #8 (generación automática de órdenes), modifica estado de la orden, y afecta inventario y schedules de mantenimiento.

---

## Revisión de Seguridad

### 1. Validación de Input

**Estado: CUBIERTO**

El pipeline de MediatR ejecuta `ValidationBehavior<TRequest, TResponse>` antes de cualquier handler. La implementación usa validación **async y paralela**:

```csharp
// src/CompuTech.Application/Common/Behaviors/ValidationBehavior.cs
var results = await Task.WhenAll(
    validators.Select(v => v.ValidateAsync(context, cancellationToken)));
var failures = results
    .SelectMany(r => r.Errors)
    .Where(f => f != null)
    .ToList();

if (failures.Count != 0)
    throw new ValidationException(failures);
```

`CompleteServiceOrderCommand` es un record con un único campo `int Id` — no hay superficie de ataque por entrada de texto libre en este endpoint.

**Hallazgo:** Durante el desarrollo se descubrió un bug donde `ValidationBehavior` llamaba `v.Validate()` sincrónicamente. Esto causaba `AsyncValidatorInvokedSynchronouslyException` en validators que usan `MustAsync` (consultas a BD). Fue corregido a `ValidateAsync` en el commit `4ef8787`.

### 2. Prevención de SQL Injection

**Estado: CUBIERTO**

El proyecto usa **Entity Framework Core 10 exclusivamente**. Todas las consultas son parametrizadas automáticamente:

```csharp
// src/CompuTech.Infrastructure/Persistence/Repositories/ServiceOrderRepository.cs
public async Task<ServiceOrder?> GetByIdAsync(int id, CancellationToken ct = default)
    => await _context.ServiceOrders
        .Include(x => x.Items)
        .FirstOrDefaultAsync(x => x.Id == id, ct);
```

No existe ningún uso de `FromSqlRaw`, `ExecuteSqlRaw` ni interpolación de strings en consultas SQL en todo el proyecto.

### 3. Exposición de Datos

**Estado: CUBIERTO**

Las entidades de dominio **nunca se serializan directamente**. Todas las respuestas de la API usan DTOs:

```
ServiceOrder (entidad)  →  ServiceOrderDetailDto (respuesta API)
```

Las propiedades con `private set` en las entidades también previenen modificación accidental desde la capa API.

### 4. Control de Estado de la Orden

**Estado: CUBIERTO — Máquina de estados en handler**

El handler valida la transición de estado antes de proceder:

```csharp
if (order.Status != ServiceOrderStatus.InProgress)
    throw new InvalidOperationException(
        $"Only InProgress orders can be completed. Current status: {order.Status}");
```

Un atacante no puede completar una orden en estado `Planned`, `Completed` o `Cancelled`. Cada transición ilegal lanza `InvalidOperationException` → HTTP 409 Conflict.

### 5. HTTPS

**Estado: CUBIERTO**

```csharp
// Program.cs
app.UseHttpsRedirection();
```

Configurado con redirección automática a HTTPS.

### 6. Fuga de Información en Errores

**Estado: PARCIALMENTE CUBIERTO**

No existe un middleware global de manejo de errores. En entorno de desarrollo (`ASPNETCORE_ENVIRONMENT=Development`), los stack traces pueden exponerse. En producción, ASP.NET Core muestra respuestas genéricas por defecto, pero no hay un `UseExceptionHandler` explícito configurado.

**Recomendación para producción:**
```csharp
app.UseExceptionHandler(appError => {
    appError.Run(async context => {
        context.Response.StatusCode = 500;
        await context.Response.WriteAsJsonAsync(new { error = "Internal server error" });
    });
});
```

### 7. Autenticación y Autorización

**Estado: FUERA DE SCOPE (Proyecto Académico)**

No se implementó autenticación (JWT, API Keys, etc.) ni autorización por roles. El endpoint `POST /api/service-orders/{id}/complete` es accesible sin credenciales. Para un ambiente de producción se requeriría:

- JWT Bearer tokens con `[Authorize]`
- Roles de técnico vs. administrador
- Validación de que el técnico autenticado es el asignado a la orden

**Nota:** Esta limitación está documentada explícitamente — es una decisión consciente dentro del alcance académico del proyecto.

### 8. Idempotencia

**Estado: PARCIALMENTE CUBIERTO**

Si el endpoint es llamado dos veces con el mismo `id`, la segunda llamada lanzará `InvalidOperationException` ("Only InProgress orders can be completed") porque la orden ya está en estado `Completed`. El sistema no falla silenciosamente — rechaza la operación duplicada.

---

## Tabla Resumen

| Aspecto de Seguridad | Estado | Mitigación |
|---|---|---|
| SQL Injection | Cubierto | EF Core con queries parametrizadas |
| Input Validation | Cubierto | FluentValidation + ValidationBehavior async |
| Data Exposure | Cubierto | DTOs en todas las respuestas |
| State Machine | Cubierto | Validación de transición en handler |
| HTTPS | Cubierto | `UseHttpsRedirection()` |
| Error Info Leakage | Parcial | Sin middleware global de excepciones |
| Autenticación | Fuera de scope | Proyecto académico |
| Idempotencia | Cubierto | Estado de orden previene re-ejecución |

---

## Bug de Seguridad Descubierto y Corregido

Durante la fase de seed/testing en producción local, se identificó y corrigió un bug en `ValidationBehavior.cs` que invocaba validadores asincrónicos de forma sincrónnica. Además, se corrigió un error en `ServiceOrderRepository.cs` donde `.Include("_items")` (referencia al backing field privado) fallaba en EF Core 10 — debía ser `.Include(x => x.Items)`.

**Commit:** `4ef8787  fix: async validation pipeline + Include navigation name + seed script`
