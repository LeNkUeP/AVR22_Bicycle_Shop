using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using UnityEngine;
using System.Net.Http;

using Debug = UnityEngine.Debug;
using Unity.VisualScripting;

/// <summary>
/// Steuerung eines Clients.
/// </summary>
public class SignalRClientController : MonoBehaviour
{
    /// <summary>
    /// Bindung an den Server und Hub
    /// </summary>
    public SignalRConnectionManager connectionManager;
    /// <summary>
    /// Name des Clients
    /// </summary>
    public string clientName = "unity";
    /// <summary>
    /// Soll SignalR benutzt werden?
    /// </summary>
    public bool signalREnabled = true;

    /// <summary>
    /// Die Entitäten des Clients. Wird zu Anfang initialisiert.
    /// </summary>
    public List<SignalREntityController> EntityControllers { get; } = new List<SignalREntityController>();

    /// <summary>
    /// Vollständiger URL zum SignalR-Hub
    /// </summary>
    private string SignalRHubUrl => $"{connectionManager.UsedSignalRServer}/{connectionManager.hub}";

    /// <summary>
    /// die Verbindung zum Hub.
    /// </summary>
    private HubConnection HubConnection = null;

    private bool running = false;

    public bool connect = false;
    private bool oldConnect = false;
    public void OnValidate()
    {
        if (running && oldConnect != connect && connect)
        {
            if (HubConnection is null)
            {
                StartSignalRAsync();
            }
            else
            {
                DisconnectEntityControllers();
                ConnectEntityControllers();
                connect = false;
            }
        }
    }

    public void Start()
    {
        running = true;
        StartVirtual();
    }

    protected virtual void StartVirtual()
    {
        Debug.Log("starting");

        if (signalREnabled)
        {
            StartSignalRAsync();
        }
    }
    
    public async Task<string> GetJwtToken(string userId)
    {
        var http = new HttpClient { BaseAddress = new Uri(connectionManager.UsedSignalRServer) };
        var httpResponse = await http.GetAsync($"/generatetoken?user={userId}");
        httpResponse.EnsureSuccessStatusCode();
        return await httpResponse.Content.ReadAsStringAsync();
    }

    async void StartSignalRAsync()
    {
        if (HubConnection == null)
        {
            Debug.Log("configuring Connection...");
            HubConnection = new HubConnectionBuilder()
                .WithUrl(SignalRHubUrl,
                /// Optionen konfigurieren - hauptsächlich aus SignalR-Beispielanwendung übernommen
                opt =>
                {
                    Debug.Log($"Setting Options...");
                    opt.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
                    opt.SkipNegotiation = true;
                    opt.AccessTokenProvider = async () =>
                    {
                        Debug.Log($"Trying to get Access Token...");
                        var token = await GetJwtToken($"Unity{Process.GetCurrentProcess().Id}_{Environment.MachineName}");
                        Debug.Log($"Access Token: {token}");
                        return token;
                    };
                })
                .AddJsonProtocol()
                .ConfigureLogging(logging =>
                {
                    logging.SetMinimumLevel(LogLevel.Information);
                    logging.AddProvider(UnityLoggingProvider.Instance);
                })
                .Build();

            Debug.Log("Attaching event...");
            HubConnection.Closed += HubConnection_Closed;

            /// Die Kommandos der einzelnen Entitäten registrieren
            foreach (var entityController in EntityControllers)
            {
                entityController.RegisterCommands(HubConnection);
            }

            Debug.Log("Starting Asynchronously...");
            await HubConnection.StartAsync();

            ConnectEntityControllers();

            Debug.Log("Started..." + $"{HubConnection.State}");
        }
        else
        {
            Debug.Log("SignalR already connected...");
        }
    }

    private void ConnectEntityControllers()
    {
        /// den Entitäten Bescheidsagen, dass wir verbunden sind.
        foreach (var entityController in EntityControllers)
        {
            entityController.Connected(HubConnection);
        }
    }
    private void DisconnectEntityControllers()
    {
        /// den Entitäten Bescheidsagen, dass wir verbunden sind.
        foreach (var entityController in EntityControllers)
        {
            entityController.Disconnect();
        }
    }

    private Task HubConnection_Closed(Exception arg)
    {
        Debug.Log($"Connection Closed with {arg}");
        HubConnection = null;
        return Task.CompletedTask;
    }
}
