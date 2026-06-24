---
name: release-module
description: >
  Fase 2 del workflow de entrega: cuando todos los issues de un módulo están mergeados en testing
  y los tests pasan, crea un PR de testing → main con todos los Closes #N del módulo y lo mergea.
  GitHub cierra automáticamente todos los issues del módulo porque main es la rama por defecto.
  Úsalo una vez por módulo completo (Customers, Equipment, Technicians, Inventory, ServiceOrders, MaintenanceSchedule).
argument-hint: "<NombreModulo> <issue1,issue2,...>"
---

# Release Module — CompuTech (Fase 2)

Libera un módulo completo de `testing` a `main`, cerrando todos sus issues automáticamente.

**Argumentos esperados:** `$ARGUMENTS`  
Formato: `<NombreModulo> <N1,N2,N3,...>` — ej. `Customers 5,6,7,8,9`

---

## Pre-condiciones

Antes de ejecutar, verificar:
1. Todos los issues del módulo tienen sus PRs mergeados en `testing`
2. `dotnet test` pasa sin errores

---

## Paso 1: Verificar que los tests pasan

```bash
cd "c:\Phoenix Projects\CompuTech"
dotnet test tests/CompuTech.Application.Tests/CompuTech.Application.Tests.csproj --no-build
```

Si los tests fallan, **no continuar**. Reportar el error al usuario para que se resuelva primero.

Si los tests pasan → continuar.

---

## Paso 2: Crear PR testing → main

Usar `mcp__github__create_pull_request` con:
- `owner`: `lalcantara-phoenix`
- `repo`: `CompuTech`
- `title`: `release([modulo]): módulo [NombreModulo] completo`
- `body`:

```
## Módulo [NombreModulo] — Release completo

Integra todos los issues del módulo [NombreModulo] desde `testing` a `main`.

Closes #N1, Closes #N2, Closes #N3, Closes #N4, Closes #N5

## Issues incluidos
- #N1 — [título del issue]
- #N2 — [título del issue]
- #N3 — [título del issue]
- #N4 — [título del issue]
- #N5 — [título del issue]

## Capas implementadas
- [x] Domain
- [x] Application (Commands + Queries + DTOs + Validators)
- [x] Infrastructure (EF Core + Repositorios)
- [x] API (Controller REST)

## Verificación
- [x] dotnet build — sin errores
- [x] dotnet test — todos los tests pasan
- [x] Swagger UI — endpoints accesibles
```

- `head`: `testing`
- `base`: `main`

> **Importante:** Los `Closes #N` en el body de este PR cerrarán los issues automáticamente al mergear, porque `main` es la rama por defecto del repositorio.

---

## Paso 3: Mergear el PR

Usar `mcp__github__merge_pull_request` con:
- `pull_number`: número del PR creado en Paso 2
- `merge_method`: `squash`
- `commit_title`: `release([modulo]): módulo [NombreModulo] completo`

---

## Paso 4: Verificar que los issues se cerraron

Usar `mcp__github__get_issue` para verificar que cada issue del módulo tenga `state: "closed"`.

Si algún issue no se cerró automáticamente (puede ocurrir con delay de GitHub), usar `mcp__github__update_issue` con `state: "closed"` como respaldo.

---

## Paso 5: Reportar

Informar al usuario:
- ✅ PR `testing → main` mergeado
- ✅ Issues cerrados: #N1, #N2, #N3, #N4, #N5
- ✅ Módulo [NombreModulo] en producción (main)
- 🔜 Siguiente módulo: [siguiente en el plan]

## Módulos y sus issues

| Módulo | Issues |
|---|---|
| Customers | #5, #6, #7, #8, #9 |
| Equipment | #10, #11, #12, #13, #14 |
| Technicians | #15, #16, #17, #18, #19 |
| Inventory | #20, #21, #22, #23, #24 |
| ServiceOrders | #25, #26, #27, #28, #29, #30, #31 |
| MaintenanceSchedule | #32, #33, #34, #35, #36 |
