using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWin.Core;

namespace SlimWin.Desktop.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly IDebloatService _debloatService;
    private CancellationTokenSource? _cancellationTokenSource;

    public MainViewModel(IDebloatService debloatService)
    {
        _debloatService = debloatService;
        Actions = new ObservableCollection<ActionItem>(debloatService.GetAvailableActions());
        ExecuteCommand = new AsyncRelayCommand(ExecuteAsync, CanExecute);
        CancelCommand = new RelayCommand(Cancel, CanCancel);
    }

    public ObservableCollection<ActionItem> Actions { get; }

    [ObservableProperty]
    public partial bool IsExecuting { get; set; }

    [ObservableProperty]
    public partial double ProgressValue { get; set; }

    [ObservableProperty]
    public partial string ProgressText { get; set; } = "Listo";

    [ObservableProperty]
    public partial bool IsIndeterminate { get; set; }

    public IAsyncRelayCommand ExecuteCommand { get; }
    public IRelayCommand CancelCommand { get; }

    private bool CanExecute() => !IsExecuting && Actions.Any(a => a.IsSelected);
    private bool CanCancel() => IsExecuting;

    private async Task ExecuteAsync()
    {
        var selectedActions = Actions.Where(a => a.IsSelected).ToList();
        if (!selectedActions.Any())
            return;

        var confirmed = await ShowConfirmationDialogAsync(selectedActions);
        if (!confirmed)
            return;

        IsExecuting = true;
        IsIndeterminate = false;
        ProgressValue = 0;
        _cancellationTokenSource = new CancellationTokenSource();
        ExecuteCommand.NotifyCanExecuteChanged();
        CancelCommand.NotifyCanExecuteChanged();

        try
        {
            var total = selectedActions.Count;
            var completed = 0;

            foreach (var action in selectedActions)
            {
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                action.Status = ActionStatus.Running;
                action.ResultMessage = string.Empty;
                ProgressText = $"Ejecutando: {action.Name}...";

                var result = await _debloatService.ExecuteActionAsync(action.Type, _cancellationTokenSource.Token);

                action.Status = result.Success ? ActionStatus.Completed : ActionStatus.Failed;
                action.ResultMessage = result.Message;

                completed++;
                ProgressValue = (double)completed / total * 100;
            }

            ProgressText = _cancellationTokenSource.Token.IsCancellationRequested
                ? "Cancelado"
                : "Todas las acciones completadas";
        }
        catch (OperationCanceledException)
        {
            ProgressText = "Cancelado";
            foreach (var action in selectedActions.Where(a => a.Status == ActionStatus.Running))
            {
                action.Status = ActionStatus.Cancelled;
                action.ResultMessage = "Cancelado por el usuario";
            }
        }
        catch (Exception ex)
        {
            ProgressText = $"Error: {ex.Message}";
        }
        finally
        {
            IsExecuting = false;
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
            ExecuteCommand.NotifyCanExecuteChanged();
            CancelCommand.NotifyCanExecuteChanged();
        }
    }

    private void Cancel()
    {
        _cancellationTokenSource?.Cancel();
    }

    private Task<bool> ShowConfirmationDialogAsync(List<ActionItem> selectedActions)
    {
        // En una implementación real, esto mostraría un diálogo de confirmación nativo
        // Por ahora retornamos true directamente para la simulación
        return Task.FromResult(true);
    }
}
