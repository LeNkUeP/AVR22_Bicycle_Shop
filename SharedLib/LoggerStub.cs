using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SharedLib;

public class LoggerStub<T>
{
    public void LogInformation(string title, string message) => LogString($"{title} : {message}");

    public void LogInformation(object obj) => LogString(obj.ToString());

    public void LogError(Exception exc, string title) => LogString("Error " + title + " : " + exc.ToString());

    private void LogString(string entry)
    {
        LogMessages.Add(entry);
        Debug.WriteLine(entry);
    }
    public List<string> LogMessages { get; set; }
}
