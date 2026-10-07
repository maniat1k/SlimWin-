# SlimWin

> **En desarrollo — proyecto sin terminar. Úsalo bajo tu propio riesgo.** No hay una versión estable ni compatibilidad validada para uso general.

SlimWin es un proyecto independiente para administrar tareas de limpieza y configuración de Windows 11 mediante una interfaz gráfica. La nueva aplicación utiliza **Avalonia, C# y .NET 10**. La migración de las operaciones del prototipo PowerShell está pendiente.

## Estado actual — 7 de octubre de 2026

La solución `SlimWin.slnx` contiene:

| Componente | Propósito |
|---|---|
| `SlimWin.Core` | Modelos de acciones, contrato del servicio y ejecución simulada |
| `SlimWin.Desktop` | Interfaz Avalonia con MVVM |
| `slimwin.ps1` | Prototipo histórico PowerShell/Windows Forms, conservado como referencia |

**La aplicación Avalonia ejecuta acciones simuladas: no realiza limpieza, backups ni cambios reales en Windows.** Los resultados simulados no demuestran que las operaciones reales funcionen.

Avance disponible:

- Cinco acciones presentadas con opciones desmarcadas por defecto.
- Estructura para progreso, cancelación y resultados por acción.
- Compilación local reportada sin errores ni advertencias.
- Apertura de la ventana comprobada manualmente en Windows.

Pendientes de validación:

- Recorrido completo de selección, ejecución simulada, cancelación y reejecución.
- **Confirmación real:** `ShowConfirmationDialogAsync` devuelve `true` automáticamente; todavía no muestra un diálogo ni espera una decisión del usuario.
- Ajustes de distribución visual, estados en español y aviso visible de modo simulación.
- Tests automatizados y CI en GitHub Actions.
- Migración y pruebas de las operaciones reales, permisos y recuperación.

## Ejecutar la GUI simulada

Requisitos para desarrollo: Windows 11, Git y **SDK de .NET 10**. La simulación no requiere elevar permisos para modificar el sistema.

Desde PowerShell:

```powershell
git clone https://github.com/maniat1k/SlimWin-.git
cd SlimWin-
dotnet build SlimWin.slnx
dotnet run --project SlimWin.Desktop
```

Si ya tienes el repositorio, ejecuta los dos últimos comandos desde su raíz.

No hay instalador publicado. Actualmente se ejecuta desde el código fuente.

## Acciones previstas

| Acción | GUI Avalonia actual |
|---|---|
| Copiar carpetas de usuario | Simulada |
| Eliminar aplicaciones preinstaladas | Simulada |
| Deshabilitar servicios | Simulada |
| Configurar ajustes de privacidad | Simulada |
| Liberar espacio en disco | Simulada |

La selección final de aplicaciones, servicios y ajustes deberá explicarse y validarse antes de habilitar operaciones reales. No se prometen mejoras de rendimiento o privacidad sin evidencia.

## Meta y próximos pasos

**Meta provisional: una primera GUI utilizable el 30/10/2026**, revisable según el resultado de las pruebas.

1. Completar la confirmación explícita y el recorrido de la GUI simulada.
2. Mejorar legibilidad, progreso, estados y presentación de resultados.
3. Incorporar tests útiles y build/tests en GitHub Actions.
4. Integrar operaciones reales de forma incremental y probarlas en una VM Windows 11.
5. Registrar estado anterior y recuperación para servicios/registro, y explicar los límites de reversibilidad de cada acción.
6. Documentar requisitos, versiones probadas y uso.

El instalador firmado y una release candidate son objetivos posteriores. La firma de código no garantiza ausencia de advertencias de SmartScreen. No se declarará una versión estable sin evidencia de validación.

## Prototipo histórico PowerShell

`slimwin.ps1` **sí contiene operaciones reales**: puede eliminar aplicaciones, deshabilitar servicios y modificar el registro. Se conserva como referencia de la migración y no es el punto de entrada de la nueva GUI.

Sus limitaciones incluyen:

- Todas las acciones seleccionadas por defecto.
- Backup limitado a Documents, Desktop y Downloads; **no es una imagen del sistema ni un rollback** de aplicaciones, servicios o registro.
- Manejo de errores y mensajes de éxito que requieren revisión.
- Cancelación y respuesta de la interfaz no garantizadas.
- Compatibilidad no validada.

Su evaluación debe hacerse en un entorno de pruebas con recuperación independiente del sistema.

## Trabajo con agentes y Git

Lee `AGENTS.md` y `CONTEXT.md` antes de comenzar una tarea relevante. Contrasta el contexto con el código y actualiza el punto de continuación cuando avance el proyecto.

`bin/`, `obj/` y la configuración local `.kilo/` están excluidos de Git. No almacenes secretos en el repositorio.

## Licencia

[MIT](LICENSE).
