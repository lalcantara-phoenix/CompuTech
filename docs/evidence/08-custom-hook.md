# Evidencia 8 — Custom Hook

**Criterio:** Implementar al menos un hook básico que agregue valor al flujo de trabajo (ejemplos: hook de validación antes de ejecutar un comando, hook de logging de operaciones).

---

## Descripción

Se implementaron **4 hooks personalizados** en `.claude/settings.json` que automatizan tareas de logging, compilación continua y registro de actividad durante el desarrollo del proyecto.

---

## Archivo de Configuración

**Ruta:** `.claude/settings.json`

---

## Hook 1 — Compilación Continua (PostToolUse / Write|Edit)

**Tipo:** `PostToolUse` con matcher `Write|Edit`  
**Ejecución:** Async (no bloquea al usuario)

Cada vez que Claude escribe o edita un archivo `.cs`, el hook ejecuta automáticamente `dotnet build` y filtra la salida para mostrar solo errores o el resultado final:

```powershell
$f=$d.tool_input.file_path
if($f -match '\.cs$' -and (Test-Path "$root\CompuTech.sln")) {
    dotnet build "$root\CompuTech.sln" --no-restore -v quiet 2>&1 |
        Where-Object { $_ -match 'error|Error|succeeded|FAILED' }
}
```

**Valor aportado:** Detecta errores de compilación inmediatamente después de cada archivo editado, sin que el desarrollador deba ejecutar `dotnet build` manualmente. Esto garantizó que cada issue implementado compilara correctamente antes de continuar al siguiente.

---

## Hook 2 — Bitácora Automática de Modificaciones (PostToolUse / Write|Edit)

**Tipo:** `PostToolUse` con matcher `Write|Edit`  
**Ejecución:** Async

Cuando Claude modifica archivos en cualquier capa del proyecto (Domain, Application, Infrastructure, API), el hook registra automáticamente la actividad en `docs/BITACORA.md`:

```powershell
$f=$d.tool_input.file_path
if($f -match 'CompuTech\.(Domain|Application|Infrastructure|API)') {
    $date = Get-Date -Format 'yyyy-MM-dd HH:mm'
    Add-Content "$root\docs\BITACORA.md" "`n- [$date] Modificado: $f" -Encoding utf8
}
```

**Valor aportado:** La bitácora `docs/BITACORA.md` se convirtió en un registro cronológico automático de todo el desarrollo. Cualquier revisor puede ver qué archivos fueron modificados y en qué orden, sin depender del historial de git.

**Ejemplo de entrada generada automáticamente:**
```
- [2026-06-24 05:32] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\Equipment.cs
- [2026-06-24 05:33] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommand.cs
```

---

## Hook 3 — Log de Comandos EF Core (PostToolUse / Bash)

**Tipo:** `PostToolUse` con matcher `Bash`  
**Ejecución:** Async

Cada vez que Claude ejecuta un comando `dotnet ef` (migraciones, actualizaciones de base de datos), el hook registra el comando completo con timestamp en `docs/04-migraciones/ef-log.txt`:

```powershell
$cmd = $d.tool_input.command
if($cmd -match 'dotnet ef') {
    $date = Get-Date -Format 'yyyy-MM-dd HH:mm:ss'
    Add-Content "$root\docs\04-migraciones\ef-log.txt" "[$date] $cmd" -Encoding utf8
}
```

**Valor aportado:** Registro auditado de todas las operaciones de migración ejecutadas durante el desarrollo. Permite reproducir exactamente el historial de cambios al esquema de base de datos.

**Contenido real del log (`docs/04-migraciones/ef-log.txt`):**

```
[2026-06-23 23:38:07] cd "c:/Phoenix Projects/CompuTech" && dotnet ef migrations add AddCustomerAndLocation ...
[2026-06-24 05:32:27] cd "C:\Phoenix Projects\CompuTech" && dotnet ef migrations add AddEquipment ...
[2026-06-24 05:40:57] cd "c:\Phoenix Projects\CompuTech" && dotnet ef migrations add AddTechnician ...
[2026-06-24 05:51:46] cd "C:\Phoenix Projects\CompuTech" && dotnet ef migrations add AddInventoryItem ...
[2026-06-24 06:11:00] cd "C:\Phoenix Projects\CompuTech" && dotnet ef migrations add AddServiceOrders ...
```

---

## Hook 4 — Resumen de Fin de Sesión (Stop)

**Tipo:** `Stop` (se ejecuta cuando Claude termina su turno)  
**Ejecución:** Async

Al finalizar cada sesión, el hook busca todos los archivos `.cs`, `.json` y `.md` modificados en las últimas 8 horas y genera un resumen en `docs/BITACORA.md`:

```powershell
$cut = (Get-Date).AddHours(-8)
$ff = Get-ChildItem $root -Recurse -File |
    Where-Object { $_.LastWriteTime -gt $cut -and
                   $_.Extension -match '\.(cs|json|md)$' -and
                   $_.FullName -notmatch 'obj|bin|\.claude' }
if($ff) {
    $count = $ff.Count
    Add-Content "$root\docs\BITACORA.md" "`n## Fin de sesión — $date ($count archivos modificados)"
    $ff | ForEach-Object {
        Add-Content "$root\docs\BITACORA.md" "- $($_.FullName)"
    }
}
```

**Valor aportado:** Genera automáticamente un resumen de sesión al final de cada jornada de trabajo, consolidando todos los archivos tocados. Esto facilita las revisiones de avance sin analizar git diff manualmente.

---

## Tabla Resumen de Hooks

| Hook | Trigger | Matcher | Propósito |
|---|---|---|---|
| Compilación continua | PostToolUse | `Write\|Edit` | `dotnet build` tras cada `.cs` editado |
| Bitácora de archivos | PostToolUse | `Write\|Edit` | Log en `BITACORA.md` por capa modificada |
| Log de migraciones EF | PostToolUse | `Bash` | Log en `ef-log.txt` por comando `dotnet ef` |
| Resumen de sesión | Stop | — | Resumen de archivos modificados en las últimas 8h |

---

## Ruta al Artefacto

```
.claude/settings.json
```

Los hooks están versionados en el repositorio y se activan automáticamente en cualquier sesión de Claude Code que abra el proyecto.
