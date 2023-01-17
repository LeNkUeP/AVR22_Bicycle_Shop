using Microsoft.Extensions.Logging;

public class UnityLoggingProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName) => new UnityLogger(categoryName);

    public void Dispose() { }

    public static UnityLoggingProvider Instance { get; } = new UnityLoggingProvider();
}
