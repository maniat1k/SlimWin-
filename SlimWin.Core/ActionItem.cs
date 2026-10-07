using CommunityToolkit.Mvvm.ComponentModel;

namespace SlimWin.Core;

public partial class ActionItem : ObservableObject
{
    public required ActionType Type { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Tooltip { get; init; }

    [ObservableProperty]
    public partial bool IsSelected { get; set; }

    [ObservableProperty]
    public partial ActionStatus Status { get; set; } = ActionStatus.Pending;

    [ObservableProperty]
    public partial string ResultMessage { get; set; } = string.Empty;
}

public enum ActionStatus
{
    Pending,
    Running,
    Completed,
    Failed,
    Cancelled
}
