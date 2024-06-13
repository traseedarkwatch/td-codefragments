using Microsoft.AspNetCore.Components;

namespace Sample.Components.Reproduction;

public abstract class TdComponentBase : ComponentBase, IDisposable
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public bool IsVisible { get; set; } = true;

    private static CancellationTokenSource _cancellationTokenSource => new CancellationTokenSource();

    protected CancellationToken CancellationToken => _cancellationTokenSource.Token;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }
    }
}
