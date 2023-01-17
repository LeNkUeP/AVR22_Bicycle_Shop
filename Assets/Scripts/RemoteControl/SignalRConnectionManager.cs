using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SignalRConnectionManager : MonoBehaviour
{
    /// <summary>
    /// Adresse bzw. Basispfad des SignalR-Servers
    /// </summary>
    public string signalRServer = "http://localhost:5000";
    /// <summary>
    /// Name des SignalR-Hubs
    /// </summary>
    public string hub = "unityremotecontrolhub";
    /// <summary>
    /// Soll der "server=..." - Parameter ausgewertet werden?
    /// </summary>
    public bool useCommandLine = true;

    /// <summary>
    /// der tatsächlich verwendete SignalR-Server
    /// </summary>
    public string UsedSignalRServer { get; private set; }

    public void Awake()
    {
        UsedSignalRServer = signalRServer;

        if (useCommandLine && Environment.GetCommandLineArgs().FirstOrDefault(a => a.StartsWith("server=")) is string serverArg)
        {
            UsedSignalRServer = serverArg.Split('=')[1];
        }
    }
}
