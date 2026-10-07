# SlimWin

> **En desarrollo — proyecto sin terminar.** La versión actual es un prototipo y no una versión estable ni validada para uso general. **Úsalo bajo tu propio riesgo.** Puede eliminar aplicaciones, deshabilitar servicios y modificar el registro de Windows. No se garantiza compatibilidad con todas las versiones o configuraciones de Windows 11.

SlimWin es un proyecto para administrar tareas de limpieza y configuración de Windows mediante una interfaz gráfica. El script actual incluye una ventana básica en PowerShell/Windows Forms; es el punto de partida para una GUI utilizable.

## Estado actual y limitaciones

- Todas las acciones aparecen seleccionadas por defecto. Revisa y desmarca las que no quieras aplicar antes de ejecutar.
- La copia de seguridad comprime Documents, Desktop y Downloads del perfil de usuario. **No es una imagen del sistema ni un mecanismo de rollback** de aplicaciones, servicios o registro.
- El manejo de errores y los mensajes de resultado necesitan revisión: un mensaje de finalización no garantiza que todas las operaciones hayan funcionado.
- La cancelación y la respuesta de la interfaz durante tareas largas todavía no están garantizadas.
- No hay una matriz de compatibilidad validada ni una versión estable publicada.

Para evaluar el prototipo, utiliza una máquina virtual o un entorno de pruebas con un respaldo independiente que permita recuperar el sistema.

## Funciones del prototipo

- Copia de carpetas de usuario.
- Eliminación de un conjunto de aplicaciones preinstaladas.
- Desactivación de servicios definidos en el script.
- Modificación de ajustes de privacidad en el registro.
- Invocación de la herramienta nativa de limpieza de disco.

Estas funciones describen el código existente; no implican mejoras de rendimiento o privacidad verificadas.

## Meta del proyecto

**Lograr una primera GUI utilizable para Windows 11**, con:

- Opciones claras, explicación de su alcance y acciones de modificación desmarcadas por defecto.
- Vista previa y confirmación de los cambios seleccionados.
- Verificación de permisos y requisitos antes de ejecutar.
- Interfaz que responda durante la ejecución, progreso visible y cancelación controlada.
- Registro de operaciones y resultados reales por acción.
- Recuperación del estado anterior de servicios y registro cuando corresponda, y límites explícitos para acciones que no puedan revertirse.
- Pruebas reproducibles en Windows 11 y documentación de instalación, uso y compatibilidad.

La elección tecnológica de la GUI y el alcance final del MVP se definirán durante el diseño. SlimWin continúa como proyecto independiente.

## Evaluar el prototipo

Requiere Windows y permisos de administrador para las operaciones que modifican el sistema. El código utiliza PowerShell y Windows Forms; las versiones compatibles aún deben validarse.

```powershell
git clone https://github.com/maniat1k/SlimWin-.git
cd SlimWin-
.\slimwin.ps1
```

Lee las limitaciones anteriores antes de ejecutarlo. La GUI actual no debe interpretarse como una versión terminada.

## Licencia

[MIT](LICENSE).
