# Bitácora OpenSpec — CompuTech

Registro cronológico de cada interacción con el framework OpenSpec durante el desarrollo de nuevas funcionalidades. Generado como evidencia académica del criterio **Custom Skill / Framework externo**.

**Framework:** OpenSpec v1.7.0  
**Repositorio:** `lalcantara-phoenix/CompuTech`  
**Fecha de inicio:** 2026-07-28

---

## Sesión 1 — Dashboard de KPIs

**Fecha:** 2026-07-28  
**Funcionalidad:** Endpoint `GET /api/dashboard` que agrega métricas de todos los módulos.

---

### Paso 1 — Crear el change

**Comando ejecutado:**
```bash
openspec new change "dashboard-kpis"
```
**Resultado:** OpenSpec creó el directorio `openspec/changes/dashboard-kpis/` con schema `spec-driven`.

---

### Paso 2 — Obtener el orden de construcción de artefactos

**Comando ejecutado:**
```bash
openspec status --change "dashboard-kpis" --json
```
**Resultado:** El status reveló el orden de dependencias:
- `proposal` → ready (sin dependencias)
- `specs` → blocked (requiere proposal)
- `design` → blocked (requiere proposal)
- `tasks` → blocked (requiere specs + design)

El campo `applyRequires` indica que `tasks` es el artefacto necesario para poder implementar.

---

### Paso 3 — Obtener instrucciones para cada artefacto

**Comando ejecutado por artefacto:**
```bash
openspec instructions <artifact-id> --change "dashboard-kpis" --json
```
Las instrucciones de cada artefacto incluyen:
- `context`: el contexto del proyecto inyectado desde `openspec/config.yaml`
- `rules`: reglas configuradas (ej. "Escribir propuestas en español", "separar tareas por capa")
- `template`: estructura exacta a seguir para el archivo
- `instruction`: guía específica del artefacto (qué incluir, qué evitar)

---

### Paso 4 — Artefactos generados

| Artefacto | Ruta | Descripción |
|---|---|---|
| `proposal.md` | `openspec/changes/dashboard-kpis/proposal.md` | Por qué, qué cambia, capabilities, no-goals, impacto |
| `specs/dashboard-kpis/spec.md` | `openspec/changes/dashboard-kpis/specs/dashboard-kpis/spec.md` | 4 requisitos con 9 escenarios WHEN/THEN |
| `design.md` | `openspec/changes/dashboard-kpis/design.md` | 5 decisiones técnicas con alternativas descartadas |
| `tasks.md` | `openspec/changes/dashboard-kpis/tasks.md` | 17 tareas en 5 grupos por capa arquitectónica |

---

### Paso 5 — Estado final de la propuesta

**Comando ejecutado:**
```bash
openspec status --change "dashboard-kpis"
```
**Resultado:**
```
Progress: 4/4 artifacts complete
[x] proposal  [x] specs  [x] design  [x] tasks
All artifacts complete!
```

**Próximo paso:** `/opsx:apply` para implementar las tareas.

---

### Observaciones sobre el flujo OpenSpec

1. **El contexto del `config.yaml` fue inyectado automáticamente** en las instrucciones de cada artefacto — las reglas del proyecto (convenciones CQRS, idioma español, separación por capas) se aplicaron sin tener que repetirlas manualmente.
2. **El orden de dependencias fue respetado** — no fue posible crear `tasks.md` hasta que `specs` y `design` estuvieran completos.
3. **Las reglas configuradas se aplicaron:** propuesta en español, tareas separadas por capa arquitectónica, sección Non-goals incluida.
4. **El spec generado es testeable:** cada requisito tiene al menos un escenario WHEN/THEN que puede convertirse directamente en un test unitario.

---
