---
name: implement-module
description: >
  Implementa un módulo completo del proyecto CompuTech recorriendo todas las capas:
  Domain (entidad + enums + repositorio) → Application (commands + queries + DTOs + validators)
  → Infrastructure (configuración EF Core + repositorio) → API (controller REST).
  Registra la evidencia completa en docs/. Úsalo para implementar un módulo de principio a fin.
argument-hint: "[NombreModulo]"
---

# Implement Module — CompuTech

Implementa el módulo completo: `$ARGUMENTS`

Este skill orquesta la implementación completa de un módulo recorriendo todas las capas de Clean Architecture. Sigue cada fase en orden — no saltes a la siguiente sin completar la anterior.

---

## Fase 1: Dominio

1. Leer `docs/01-especificacion/especificacion.md` — sección de la entidad `$ARGUMENTS`.
2. Crear la entidad de dominio siguiendo el patrón DDD (factory method, `private set`, constructor privado).
3. Crear los enums relacionados según el spec.
4. Crear la interfaz `I$ARGUMENTSRepository` con métodos base + métodos específicos del módulo.

**Archivos esperados:**
- `CompuTech.Domain/Entities/$ARGUMENTS.cs`
- `CompuTech.Domain/Enums/[Enum].cs` (por cada enum relacionado)
- `CompuTech.Domain/Interfaces/I$ARGUMENTSRepository.cs`

---

## Fase 2: Application — Commands

Para cada operación de escritura identificada en el spec:

1. `Create$ARGUMENTSCommand` + Handler + Validator
2. `Update$ARGUMENTSCommand` + Handler + Validator
3. `Deactivate$ARGUMENTSCommand` + Handler
4. Cualquier command adicional específico del módulo (ej. `CompleteServiceOrderCommand`, `AddServiceOrderItemCommand`)

**Estructura:** `CompuTech.Application/$ARGUMENTS/Commands/`

---

## Fase 3: Application — Queries y DTOs

1. `Get$ARGUMENTSByIdQuery` + Handler
2. `GetAll$ARGUMENTSsQuery` + Handler
3. Queries específicas del módulo (ej. `GetEquipmentByCustomerQuery`, `GetServiceOrdersByStatusQuery`)
4. DTOs de respuesta

**Estructura:** `CompuTech.Application/$ARGUMENTS/Queries/` y `CompuTech.Application/$ARGUMENTS/DTOs/`

---

## Fase 4: Infrastructure

1. Clase de configuración EF Core: `CompuTech.Infrastructure/Persistence/Configurations/$ARGUMENTSConfiguration.cs`
   - Fluent API para tipos, longitudes, unicidad, FKs
2. Implementación del repositorio: `CompuTech.Infrastructure/Persistence/Repositories/$ARGUMENTSRepository.cs`
   - Implementar todos los métodos de la interfaz
3. Registrar en `AppDbContext.cs`: agregar `DbSet<$ARGUMENTS>`
4. Registrar el repositorio en `CompuTech.Infrastructure/DependencyInjection.cs`

---

## Fase 5: API

1. Generar `CompuTech.API/Controllers/$ARGUMENTSController.cs`
   - Endpoints CRUD + endpoints específicos del módulo
   - Documentación XML para Swagger
2. Verificar que todos los commands/queries referenciados existen

---

## Fase 6: Documentación y evidencia

1. Crear `docs/03-implementacion/$ARGUMENTS.md` con:
   - Lista completa de archivos creados
   - Descripción de cada command/query implementado
   - Reglas de negocio aplicadas
2. Actualizar `docs/BITACORA.md` con la entrada del día

---

## Notas de implementación

- Aplicar las **8 reglas de negocio** del spec durante la implementación de handlers.
- No avanzar a la siguiente fase si la anterior tiene errores de compilación.
- Mapeo manual en handlers — no AutoMapper.
- Usar `async/await` + `CancellationToken` en todos los métodos.
- Las relaciones de navegación en EF Core se configuran en `[Entidad]Configuration`, no en la entidad.
