---
name: submit-feature-pr
description: >
  Fase 1 del workflow de entrega: push de la rama feature/* al remoto, crea PR hacia testing
  con Closes #N para traceabilidad, detecta y resuelve conflictos automáticamente vía rebase,
  y mergea con squash. El issue queda ABIERTO — se cierra en Fase 2 (/release-module) cuando
  el módulo completo llega a main.
argument-hint: "<issue_number> <branch_name>"
---

# Submit Feature PR — CompuTech (Fase 1)

Ejecuta el ciclo completo de entrega de un issue ya implementado a la rama `testing`.

**Argumentos esperados:** `$ARGUMENTS`  
Formato: `<número_issue> <nombre_rama>` — ej. `7 customers-queries`

---

## Paso 1: Push de la rama

```bash
git push origin feature/$BRANCH
```

Si el remote no existe aún, usar:
```bash
git push -u origin feature/$BRANCH
```

---

## Paso 2: Crear el PR hacia testing

Usar `mcp__github__create_pull_request` con:
- `owner`: `lalcantara-phoenix`
- `repo`: `CompuTech`
- `title`: extraer del issue usando `mcp__github__get_issue` (issue #N)
- `body`:

```
## Issue relacionado
Closes #N

## Descripción del cambio
[Descripción breve de lo implementado]

## Capas modificadas
- [ ] Domain
- [ ] Application
- [ ] Infrastructure
- [ ] API
- [ ] Tests

## Checklist
- [ ] El código compila sin errores (dotnet build)
- [ ] Los tests pasan (dotnet test)
- [ ] No hay lógica de negocio en Infrastructure ni en API
- [ ] Las validaciones están en el Validator, no en el Handler
```

- `head`: `feature/$BRANCH`
- `base`: `testing`

> **Nota:** `Closes #N` en el body sirve solo para traceabilidad (link PR ↔ issue). El issue NO se cerrará porque `testing` no es la rama por defecto.

---

## Paso 3: Verificar si el PR es mergeable

Usar `mcp__github__get_pull_request` para obtener el estado del PR recién creado.

Esperar ~3 segundos para que GitHub calcule el estado mergeable, luego verificar:
- `mergeable_state == "clean"` → continuar al Paso 4
- `mergeable_state == "dirty"` → ejecutar Paso 3b (resolver conflictos)
- `mergeable_state == "unknown"` → esperar 5 segundos más y volver a verificar

### Paso 3b: Resolver conflictos automáticamente

```bash
git fetch origin testing
git rebase origin/testing
```

Si hay conflictos (exit code != 0):
```bash
git checkout --theirs .
git add .
git rebase --continue --no-edit
```

Si el rebase sigue fallando tras el primer `--continue`, repetir:
```bash
git checkout --theirs .
git add .
git rebase --continue --no-edit
```

Una vez resueltos:
```bash
git push --force-with-lease origin feature/$BRANCH
```

Esperar ~5 segundos y volver a verificar el estado mergeable del PR.

---

## Paso 4: Mergear el PR

Usar `mcp__github__merge_pull_request` con:
- `pull_number`: número del PR creado en Paso 2
- `merge_method`: `squash`
- `commit_title`: título descriptivo del issue
- `commit_message`: incluir `Closes #N` como referencia

---

## Paso 5: Reportar

Informar al usuario:
- ✅ Rama pushed: `feature/$BRANCH`
- ✅ PR #[numero] mergeado a `testing`
- ⏳ Issue #N permanece ABIERTO (se cerrará con `/release-module` al completar el módulo)

**NO cerrar el issue.** El cierre ocurre en Fase 2 cuando se hace PR `testing → main`.
