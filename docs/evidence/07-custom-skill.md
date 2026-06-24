# Evidencia 7 — Custom Skill

**Criterio:** Crear un skill propio útil para el proyecto (ejemplos: generador de entidades DDD, scaffold de handler CQRS, generador de migraciones EF). Incluir el archivo del skill en el repositorio.

---

## Descripción

Se crearon **9 skills personalizados** para el proyecto CompuTech, todos almacenados en `.claude/skills/*/SKILL.md`. Estos skills aceleraron el desarrollo desde el primer día y fueron diseñados específicamente para la arquitectura Clean Architecture + CQRS del proyecto.

---

## Skills Creados

### 1. `/implement-module [NombreModulo]`

**Archivo:** `.claude/skills/implement-module/SKILL.md`

El skill más complejo y central del proyecto. Orquesta la implementación completa de un módulo recorriendo las 6 fases de Clean Architecture:

1. **Domain** — entidad DDD + enums + interfaz repositorio
2. **Application Commands** — Command + Handler + Validator por operación
3. **Application Queries + DTOs** — Query + Handler + DTO de respuesta
4. **Infrastructure** — configuración Fluent API + repositorio EF Core
5. **API** — controller REST con todos los endpoints + XML comments
6. **Documentación** — actualiza `docs/BITACORA.md` + crea `docs/03-implementacion/[Modulo].md`

**Fragmento del SKILL.md:**

```markdown
## Fase 1: Dominio

1. Leer docs/01-especificacion/especificacion.md — sección de la entidad $ARGUMENTS.
2. Crear la entidad de dominio siguiendo el patrón DDD (factory method, private set, constructor privado).
3. Crear los enums relacionados según el spec.
4. Crear la interfaz I$ARGUMENTSRepository con métodos base + métodos específicos del módulo.

Archivos esperados:
- CompuTech.Domain/Entities/$ARGUMENTS.cs
- CompuTech.Domain/Enums/[Enum].cs (por cada enum relacionado)
- CompuTech.Domain/Interfaces/I$ARGUMENTSRepository.cs
```

**Uso demostrado:**
```
/implement-module ServiceOrder
/implement-module MaintenanceSchedule
/implement-module Customer
```

---

### 2. `/submit-feature-pr [issue] [rama]`

**Archivo:** `.claude/skills/submit-feature-pr/SKILL.md`

Fase 1 del GitFlow: entrega de un issue implementado a `testing`. Encapsula:
- Push de la rama feature al remoto
- Creación del PR con body estándar y `Closes #N`
- Detección y resolución automática de conflictos via rebase
- Squash merge del PR

**Uso:** Invocado automáticamente ~38 veces durante el desarrollo, una por issue.

---

### 3. `/release-module [Modulo] [issues]`

**Archivo:** `.claude/skills/release-module/SKILL.md`

Fase 2 del GitFlow: liberación de un módulo completo de `testing` a `main`. Ejecuta:
1. `dotnet test` para verificar que los tests pasan
2. Crea PR `testing → main` con todos los `Closes #N`
3. Squash merge → GitHub cierra los issues automáticamente
4. Verifica el cierre de cada issue

**Uso:** Invocado 7 veces (una por módulo + docs/hotfixes).

---

### 4. `/design-tests [Modulo]`

**Archivo:** `.claude/skills/design-tests/SKILL.md`

Diseña y genera la suite completa de pruebas unitarias del módulo:
- Tests de handlers CQRS (commands y queries)
- Tests de validators FluentValidation
- Tests de reglas de negocio con casos borde
- Usa xUnit, Moq y FluentAssertions

**Resultado:** 44 pruebas unitarias, 0 fallos.

---

### 5. `/scaffold-entity [Entidad]`

**Archivo:** `.claude/skills/scaffold-entity/SKILL.md`

Genera el esqueleto DDD completo antes de implementar commands/queries:
- Clase de dominio con factory method y `private set`
- Enums relacionados
- Interfaz de repositorio
- Entrada en `docs/BITACORA.md`

---

### 6. `/add-cqrs-handler command|query [Nombre]`

**Archivo:** `.claude/skills/add-cqrs-handler/SKILL.md`

Genera un handler individual completo:
- Record del Command/Query (`IRequest<int>` o `IRequest`)
- Handler con `IRequestHandler`
- Validator con `AbstractValidator` (solo para commands)

---

### 7. `/add-migration [Descripcion]`

**Archivo:** `.claude/skills/add-migration/SKILL.md`

Genera migración EF Core con validación de seguridad:
- Ejecuta `dotnet ef migrations add`
- Verifica que no haya cambios destructivos (`DropTable`, `DropColumn`)
- Registra la evidencia en `docs/04-migraciones/`

**Migraciones generadas via este skill:**

| Fecha | Migración |
|---|---|
| 2026-06-23 23:38 | `AddCustomerAndLocation` |
| 2026-06-24 05:32 | `AddEquipment` |
| 2026-06-24 05:40 | `AddTechnician` |
| 2026-06-24 05:51 | `AddInventoryItem` |
| 2026-06-24 06:11 | `AddServiceOrders` |

---

### 8. `/api-endpoint [Modulo]`

**Archivo:** `.claude/skills/api-endpoint/SKILL.md`

Genera el controller REST completo con todos los endpoints CRUD + endpoints específicos del negocio, usando IMediator y documentación XML para Swagger.

---

### 9. `/load-issues`

**Archivo:** `.claude/skills/load-issues/SKILL.md`

Carga el backlog completo del proyecto como issues de GitHub. Invoca al agente `github-project-manager` para crear milestones, labels e issues en orden.

---

## Ubicación en el Repositorio

```
.claude/
  skills/
    implement-module/SKILL.md
    submit-feature-pr/SKILL.md
    release-module/SKILL.md
    design-tests/SKILL.md
    scaffold-entity/SKILL.md
    add-cqrs-handler/SKILL.md
    add-migration/SKILL.md
    api-endpoint/SKILL.md
    load-issues/SKILL.md
```

Todos los archivos están versionados en el repositorio y disponibles para cualquier desarrollador del equipo.

## Commit de Evidencia

Los skills fueron creados en las primeras sesiones de desarrollo (2026-06-23) y están presentes en todos los commits desde el inicio:

```
docs/BITACORA.md:
## 2026-06-23 — Configuración de agentes y skills de Claude Code
Skills creadas (.claude/skills/):
- /scaffold-entity [Entidad]
- /add-migration [Descripcion]
- /add-cqrs-handler command|query [Nombre]
- /api-endpoint [Modulo]
- /implement-module [Modulo]
- /design-tests [Modulo]
```
