using System;

/// <summary>
/// Simple IDisposable that can be created from an Action 
/// </summary>
public class Disposable : IDisposable
{
    private readonly Action _onDispose;
    private bool _wasDisposed;

    public Disposable(Action onDispose)
    {
        _onDispose = onDispose;
    }
    
    public void Dispose()
    {
        if (_wasDisposed)
        {
            return;
        }
        
        _onDispose?.Invoke();
        _wasDisposed = true;
    }
}
