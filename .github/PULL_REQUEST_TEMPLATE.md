## Issue relacionado

Closes #[número]

---

## Descripción del cambio

[Qué se implementó y por qué]

---

## Capas modificadas

- [ ] Domain
- [ ] Application
- [ ] Infrastructure
- [ ] API
- [ ] Tests

---

## Criterios de aceptación

> Copia los criterios del issue y marca los completados.

- [ ] 
- [ ] 

---

## Checklist

- [ ] El código compila sin errores (`dotnet build`)
- [ ] Los tests pasan (`dotnet test`)
- [ ] No hay lógica de negocio en Infrastructure ni en API
- [ ] Los handlers no exponen entidades de dominio directamente (usan DTOs)
- [ ] Las validaciones están en el Validator, no en el Handler
- [ ] La bitácora fue actualizada (`docs/BITACORA.md`)

---

## Notas para el revisor

[Contexto adicional, decisiones de diseño, o puntos que requieren atención especial]
