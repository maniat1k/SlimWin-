using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SlimWin.Core;

public sealed class SimulatedDebloatService : IDebloatService
{
    private static readonly Random _random = new();

    private readonly List<ActionItem> _actions = new()
    {
        new ActionItem
        {
            Type = ActionType.Backup,
            Name = "Crear copia de seguridad",
            Description = "Comprime Documents, Desktop y Downloads del perfil de usuario en C:\\Backup_Windows",
            Tooltip = "Crea un archivo ZIP por carpeta con timestamp. No es imagen de sistema ni rollback de apps/servicios/registro."
        },
        new ActionItem
        {
            Type = ActionType.RemoveApps,
            Name = "Eliminar aplicaciones preinstaladas",
            Description = "Desinstala apps como Xbox, Solitaire, Noticias, People, Zune, 3DBuilder",
            Tooltip = "Elimina paquetes Appx para el usuario actual y todos los usuarios. Requiere admin."
        },
        new ActionItem
        {
            Type = ActionType.DisableServices,
            Name = "Deshabilitar servicios innecesarios",
            Description = "Detiene y deshabilita DiagTrack, dmwappushservice, XboxGipSvc, xbgm, XblAuthManager, XblGameSave",
            Tooltip = "Cambia tipo de inicio a Deshabilitado y detiene el servicio. No hay rollback automático implementado."
        },
        new ActionItem
        {
            Type = ActionType.ConfigurePrivacy,
            Name = "Configurar ajustes de privacidad",
            Description = "Desactiva telemetría (AllowTelemetry=0) y consentimiento de Cortana en registro",
            Tooltip = "Modifica HKLM y HKCU. Requiere admin. No hay rollback automático implementado."
        },
        new ActionItem
        {
            Type = ActionType.CleanStorage,
            Name = "Liberar espacio en disco",
            Description = "Invoca cleanmgr.exe /sagerun:1 (limpieza de disco programada)",
            Tooltip = "Ejecuta la herramienta nativa de Windows. Puede tardar varios minutos."
        }
    };

    public IReadOnlyList<ActionItem> GetAvailableActions() => _actions;

    public async Task<ActionResult> ExecuteActionAsync(ActionType actionType, CancellationToken cancellationToken = default)
    {
        var action = _actions.Find(a => a.Type == actionType);
        if (action == null)
        {
            return new ActionResult(actionType, false, "Acción no encontrada");
        }

        try
        {
            // Simular trabajo: 2-5 segundos con progreso
            var steps = _random.Next(4, 10);
            for (int i = 1; i <= steps; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Delay(_random.Next(200, 600), cancellationToken);
            }

            // Simular fallo ocasional (10% de probabilidad) para demostrar manejo de errores
            if (_random.Next(10) == 0)
            {
                var ex = new InvalidOperationException("Error simulado: operación falló");
                return new ActionResult(actionType, false, "Falló: " + ex.Message, ex);
            }

            return new ActionResult(actionType, true, "Completado correctamente (simulado)");
        }
        catch (OperationCanceledException)
        {
            return new ActionResult(actionType, false, "Cancelado por el usuario", new OperationCanceledException());
        }
        catch (Exception ex)
        {
            return new ActionResult(actionType, false, "Error: " + ex.Message, ex);
        }
    }
}
