namespace SlimWin.Core;

public record ActionResult(
    ActionType ActionType,
    bool Success,
    string Message,
    Exception? Exception = null
);
