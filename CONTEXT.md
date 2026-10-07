# Contexto del proyecto

## Bootstrap

**Estado:** COMPLETADO

Estados permitidos: `PENDIENTE` | `COMPLETADO`

Cuando esté `PENDIENTE`, el agente debe inspeccionar primero el repositorio y luego completar, si hace falta, una entrevista adaptativa de hasta 10 preguntas, siempre una por vez. Tras la confirmación humana, debe consolidar lo aprendido en este archivo y cambiar el estado a `COMPLETADO`.


## Objetivo actual

Lograr una primera GUI utilizable para Windows 11 usando Avalonia (.NET 10 LTS, XAML, nativo), migrando la lógica de PowerShell a C#. Primera GUI = navegable con 5 acciones simuladas, opciones desmarcadas por defecto, confirmación, progreso, cancelación y resultados por acción. Instalador firmado y release candidate son objetivos posteriores.


## Estado actual

### HECHOS VERIFICADOS

- Repositorio en `C:\dev\SlimWin-`, Git en rama `main`, sincronizado con `origin/main`.
- Prototipo actual: `slimwin.ps1` (PowerShell + Windows Forms, 194 líneas) — 5 funciones: backup carpetas usuario, eliminar apps preinstaladas, deshabilitar servicios, configurar privacidad en registro, limpieza disco.
- README declara "En desarrollo — proyecto sin terminar", "úsalo bajo tu propio riesgo", MIT License (Copyright 2024 Marcelo Lemos).
- Licencia MIT.
- `slimwin.ps1` se conserva como histórico/referencia; **no se modifica**.
- Avalonia 12+ soporta .NET 10 (LTS hasta nov 2028); .NET 8 LTS termina soporte nov 2026.
- .NET 10 LTS: release nov 2025, fin de soporte nov 2028 (fuente: Microsoft Support Policy).

### DECISIONES

- **Stack GUI:** Avalonia (.NET 10 LTS, XAML, nativo).
- **Usuario objetivo:** Usuarios generales (no técnicos).
- **Definition of Done (primera GUI):** Aplicación que compile y sea navegable con 5 acciones simuladas, checkboxes desmarcadas por defecto, confirmación antes de ejecutar, barra de progreso, cancelación controlada, log de resultados por acción. Sin modificaciones reales del sistema.
- **Definition of Done (post-MVP):** Release candidate — tests automatizados + matriz compatibilidad Windows 11 + instalador .exe firmado + documentación usuario.
- **Testing:** CI/CD GitHub Actions (primario) + tests de integración PowerShell (secundario).
- **Distribución (post-MVP):** Instalador .exe simple (NSIS/Inno Setup) con certificado de firma.
- **Certificado de firma:** No disponible — no bloquea desarrollo; se gestiona para release.
- **.NET Runtime:** .NET 10 LTS (recomendado por Avalonia 12+, evita fin de soporte .NET 8 en nov 2026).
- **Arquitectura lógica:** Migración completa a C#; `slimwin.ps1` queda como histórico (solo lectura).
- **Funciones MVP (simuladas):** Todas 5 del prototipo actual (backup, apps, servicios, privacidad, limpieza).
- **Git workflow:** `main` protegida + feature branches + PR obligatorio + review (preparado para equipo futuro).
- **Riesgo conocido:** Permisos de administrador obligatorios para operaciones de sistema.

### NO VERIFICADO

- Obtención de certificado de firma EV/estándar (costo, proveedor, proceso).
- Matriz de compatibilidad validada (Windows 11 22H2 / 23H2 / 24H2).
- Diseño UX detallado para usuarios no técnicos (tooltips, confirmaciones, progreso, cancelación, rollback).
- Estrategia de rollback real para acciones irreversibles (servicios, registro, apps).
- Manejo de falsos positivos antivirus / SmartScreen (firma reduce pero no elimina advertencias).
- Pruebas de rendimiento y tamaño final del instalador/binario.

## Trabajo reciente

Bootstrap inicial completado: inspección del repositorio, lectura de AGENTS.md, README.md, slimwin.ps1, LICENSE, .gitignore; entrevista adaptativa de 10 preguntas; consolidación de decisiones en CONTEXT.md. Correcciones aplicadas: .NET 10 LTS, sin estimación de tamaño, SmartScreen corregido, instalador/RC como post-MVP.

## Punto exacto de continuación

Próxima tarea técnica: crear solución Avalonia (.NET 10) con estructura de proyecto (`SlimWin.sln`, `SlimWin.Core`, `SlimWin.Desktop`), implementar UI con 5 acciones simuladas (checkboxes desmarcadas, confirmación, progreso, cancelación, log por acción). Mantener `slimwin.ps1` intacto en raíz. No ejecutar modificaciones reales del sistema.

## Próxima acción

Crear `SlimWin.sln` con proyecto Avalonia (`SlimWin.Desktop` targeting net10.0) y proyecto de lógica (`SlimWin.Core` targeting net10.0), añadir referencias (Avalonia, CommunityToolkit.Mvvm). Implementar MainWindow con lista de acciones, checkboxes, botón ejecutar con confirmación, progress bar, cancelación, y panel de resultados.

## Bloqueos y riesgos actuales

- **Permisos admin:** Todas las operaciones de debloat requieren elevación; la GUI debe detectar y solicitar UAC correctamente (para fase real, no simulada).
- **Rollback real:** Deshabilitar servicios y modificar registro no tienen rollback automático implementado; necesita diseño (backup de estado previo, restore) — fase post-MVP.
- **Compatibilidad:** Sin matriz validada; riesgo de romper sistemas en versiones específicas de Windows 11.
- **SmartScreen:** Firma de código reduce pero no garantiza ausencia de advertencias; requiere reputación y certificado EV para mejor resultado.

## Deuda técnica

- `slimwin.ps1` mantiene lógica legacy sin tests; migrar a C# con tests unitarios/integración.
- PowerShell actual usa `ErrorAction SilentlyContinue` — pierde visibilidad de errores reales; nuevo diseño debe logar éxito/fallo por acción.
- Cancelación actual solo frena siguiente iteración; necesita cancellation token propagado a todas las operaciones async.
- UI actual no respeta DPI alto ni escalado; Avalonia maneja esto nativamente pero requiere verificación.

## Referencias

- `slimwin.ps1` — prototipo histórico (no modificar)
- `README.md` — descripción del proyecto y limitaciones
- `AGENTS.md` — contrato operativo del proyecto
- `.gitignore` — exclusiones estándar
- `.kilo/agents/data.md` — configuración agente datos (no usado en este proyecto)
- Microsoft .NET Support Policy: .NET 10 LTS hasta nov 2028, .NET 8 LTS hasta nov 2026
- Avalonia 12+ docs: .NET 10 recomendado, templates .NET 10 disponibles desde ene 2026

## Última actualización

2026-10-07 - Bootstrap completado + correcciones: .NET 10 LTS, sin estimación MB, SmartScreen corregido, instalador/RC post-MVP.
