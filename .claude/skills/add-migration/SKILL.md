---
name: add-migration
description: >
  Genera una migración de Entity Framework Core para el proyecto CompuTech,
  verifica que no haya cambios destructivos accidentales (DropTable, DropColumn),
  y registra la evidencia en docs/04-migraciones/. Ejecutar solo cuando el
  DbContext o las configuraciones Fluent API hayan cambiado.
argument-hint: "[DescripcionEnPascalCase]"
---

# Add Migration — CompuTech

Genera y valida la migración EF Core con descripción: `$ARGUMENTS`

## Pasos a ejecutar

1. **Verificar prerequisitos** — confirmar que la solución compila antes de migrar:
   ```
   dotnet build CompuTech.sln
   ```
   Si hay errores de compilación, detener y reportar.

2. **Generar la migración:**
   ```
   dotnet ef migrations add $ARGUMENTS --project CompuTech.Infrastructure --startup-project CompuTech.API --output-dir Persistence/Migrations
   ```

3. **Leer el archivo generado** en `CompuTech.Infrastructure/Persistence/Migrations/` y verificar:
   - ❌ No hay `DropTable(...)` no intencionado
   - ❌ No hay `DropColumn(...)` no intencionado
   - ✅ Las columnas `nullable: true` corresponden a campos `?` del spec
   - ✅ Las FKs tienen `onDelete: ReferentialAction.Restrict` o `Cascade` según las relaciones
   - ✅ Los índices únicos están en campos que el spec marca como únicos (Email, SKU, SerialNumber)
   - ✅ Los valores por defecto están correctos

4. **Si hay problemas** encontrados en la validación:
   - Eliminar la migración: `dotnet ef migrations remove --project CompuTech.Infrastructure --startup-project CompuTech.API`
   - Reportar exactamente qué está mal y qué configuración Fluent API hay que corregir
   - NO aplicar la migración

5. **Si la migración es correcta**, crear documento de evidencia en `docs/04-migraciones/`:
   - Nombre del archivo: `[fecha-hoy]-$ARGUMENTS.md`
   - Contenido: descripción del cambio, tablas/columnas afectadas, y el contenido del método `Up()` de la migración

6. **Actualizar** `docs/BITACORA.md` con la migración generada.

## Notas importantes

- Nunca ejecutar `dotnet ef database update` automáticamente — eso lo hace el desarrollador manualmente.
- Si el proyecto no tiene aún `AppDbContext` o la cadena de conexión, reportar qué falta antes de continuar.
- La cadena de conexión para LocalDB es: `Server=(localdb)\\mssqllocaldb;Database=CompuTechDb;Trusted_Connection=True;`
