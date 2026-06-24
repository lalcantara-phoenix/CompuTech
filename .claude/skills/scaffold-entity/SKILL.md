---
name: scaffold-entity
description: >
  Genera el esqueleto completo de una entidad DDD para el proyecto CompuTech:
  clase de dominio con factory method, enums relacionados, interfaz de repositorio
  y documentación en la bitácora. Úsalo antes de implementar commands/queries.
argument-hint: "[NombreEntidad]"
---

# Scaffold Entity — CompuTech

Genera la estructura DDD completa para la entidad `$ARGUMENTS` en el proyecto CompuTech.

## Pasos a ejecutar

1. **Leer la especificación** de la entidad en `docs/01-especificacion/especificacion.md` para obtener campos, tipos y relaciones exactas.

2. **Generar la entidad de dominio** en `CompuTech.Domain/Entities/$ARGUMENTS.cs`:
   - Propiedades con `private set`
   - Constructor privado vacío (para EF Core)
   - Factory method estático `Create(...)` con los parámetros requeridos
   - Métodos de actualización para campos modificables (`Update(...)`, `Deactivate()`)

3. **Generar enums** relacionados (si los hay) en `CompuTech.Domain/Enums/`:
   - Un archivo por enum según el spec
   - Con los valores exactos del spec (incluir valores numéricos si aplica, como `MaintenanceFrequency`)

4. **Generar interfaz de repositorio** en `CompuTech.Domain/Interfaces/I$ARGUMENTSRepository.cs`:
   ```csharp
   public interface I$ARGUMENTSRepository
   {
       Task<$ARGUMENTS?> GetByIdAsync(int id, CancellationToken ct = default);
       Task<IReadOnlyList<$ARGUMENTS>> GetAllAsync(CancellationToken ct = default);
       Task AddAsync($ARGUMENTS entity, CancellationToken ct = default);
       Task UpdateAsync($ARGUMENTS entity, CancellationToken ct = default);
       Task<bool> ExistsAsync(int id, CancellationToken ct = default);
   }
   ```
   Agrega métodos adicionales según las queries del módulo (ej. `GetByEmailAsync`, `GetBySerialNumberAsync`).

5. **Documentar** en `docs/03-implementacion/dominio.md`:
   - Nombre de la entidad
   - Archivos creados con sus rutas
   - Métodos del repositorio generados

6. **Actualizar** `docs/BITACORA.md` con la entrada del día de hoy indicando qué entidad fue scaffoldeada.

## Notas importantes

- Respetar las reglas DDD: la entidad es la única responsable de su propio estado.
- No agregar referencias a EF Core, MediatR ni nada externo en el proyecto Domain.
- Si la entidad tiene relaciones de navegación, incluir las propiedades pero sin lógica de carga.
- El factory method `Create(...)` solo recibe los campos requeridos (no los opcionales con `?`).
