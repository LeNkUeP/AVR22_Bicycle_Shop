using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using SharedLib;
//using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanelBlazorServer.Components;

public class UnityRemoteControlComponent : HubConnectedComponent
{
    /* [Inject] */
    private LoggerStub<UnityRemoteControlComponent> Logger { get; set; } = new LoggerStub<UnityRemoteControlComponent>();
    /// <summary>
    /// payload für die unregistrierten Befehle
    /// </summary>
    internal string CommandText { get; set; }

    /// <summary>
    /// Festlegen der Farbe
    /// </summary>
    internal string Color
    {
        get => _Color;
        set
        {
            _Color = value;
            SetColor(_Color);
        }
    }
    private string _Color;

    /// <summary>
    /// Festlegen der Animation
    /// </summary>
    internal string Animation
    {
        get => _Animation;
        set
        {
            _Animation = value;
            SetAnimation(_Animation);
        }
    }
    private string _Animation;

    /// <summary>
    /// Die Menge der zur Verfügung stehenden Animationen
    /// </summary>
    public HashSet<string> Animations = new HashSet<string>();

    /// <summary>
    /// Das Protokoll, dass angezeigt werden soll
    /// </summary>
    public List<string> LogOutput { get; set; } = new List<string>();
    [Inject]
    protected IJSRuntime JSRuntime { get; set; }
    [Inject]
    protected NavigationManager Navigation { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Logger.LogMessages = LogOutput;
    }

    /// <summary>
    /// Unregistrierten Befehl auslösen
    /// </summary>
    /// <returns></returns>
    internal async Task Command() => await Connection.InvokeAsync("Command", CommandText);

    #region die Registrierten Befehle

    /// <summary>
    /// Audiodatei abspielen lassen
    /// </summary>
    /// <param name="audio"></param>
    /// <returns></returns>
    internal async Task SetAudio(string audio) => await Connection.InvokeAsync("Audio", audio);
    /// <summary>
    /// Audio stumm schalten
    /// </summary>
    /// <returns></returns>
    internal async Task SetAudioMute() => await Connection.InvokeAsync("Audio", "mute");
    /// <summary>
    /// Audio wieder laut machen
    /// </summary>
    /// <returns></returns>
    internal async Task SetAudioUnmute() => await Connection.InvokeAsync("Audio", "unmute");
    /// <summary>
    /// Audio stoppen
    /// </summary>
    /// <returns></returns>
    internal async Task StopAudio() => await Connection.InvokeAsync("Audio", "stop");

    /// <summary>
    /// Farbe setzen
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    internal async Task SetColor(string color) => await Connection.InvokeAsync("Color", color);

    /// <summary>
    /// Animation setzen
    /// </summary>
    /// <param name="animation"></param>
    /// <returns></returns>
    internal async Task SetAnimation(string animation) => await Connection.InvokeAsync("Animation", animation);


    #endregion
}
