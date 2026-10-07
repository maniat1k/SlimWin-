using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SlimWin.Core;

public interface IDebloatService
{
    IReadOnlyList<ActionItem> GetAvailableActions();
    Task<ActionResult> ExecuteActionAsync(ActionType actionType, CancellationToken cancellationToken = default);
}
