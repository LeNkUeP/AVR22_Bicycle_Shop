using System;

public class UnityLoggerScope<TState> : IDisposable
{
    private TState State { get; set; } //TODO: sollte benutzt werden für irgendwas.
    public UnityLoggerScope(TState state) => State = state;
    public void Dispose() { }
}
