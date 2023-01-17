using System;
using Microsoft.Extensions.Logging;
using Debug = UnityEngine.Debug;

public class UnityLogger : Microsoft.Extensions.Logging.ILogger
{
    public string CategoryName { get; private set; }

    public UnityLogger(string categoryName) => CategoryName = categoryName;

    public IDisposable BeginScope<TState>(TState state) => new UnityLoggerScope<TState>(state);

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        => Debug.Log($"<{logLevel}> [{eventId}] {state} {exception} {formatter(state, exception)}");
}
