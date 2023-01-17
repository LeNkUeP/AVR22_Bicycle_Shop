//using Blazor.Extensions.Logging;
//using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace UnityRemoteControl
{
    public class ILogger<T>
    {
        internal void LogInformation(string title, string message) => LogString($"{title} : {message}");

        internal void LogInformation(object obj) => LogString(obj.ToString());

        internal void LogError(Exception exc, string title) => LogString("Error " + title + " : " + exc.ToString());

        private void LogString(string entry) => LogMessages.Add(entry);
        public List<string> LogMessages { get; set; }
    }
}
