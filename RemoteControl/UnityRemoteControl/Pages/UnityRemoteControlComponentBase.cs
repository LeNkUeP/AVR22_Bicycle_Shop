//using Microsoft.AspNetCore.Blazor;
//using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
//using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityRemoteControl.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.SignalR.Client;
using BlazorSignalR;

namespace UnityRemoteControl
{
    public class UnityRemoteControlComponentBase : ComponentBase
    {
        [Inject]
        private HttpClient Http { get; set; }
        private HubConnection Connection;
        [Inject]
        protected IJSRuntime JsRuntime { get; set; }
        [Inject]
        private HubConnectionBuilder _hubConnectionBuilder { get; set; }

        [Inject]
        public IUriHelper UriHelper { get; set; }

        private async void InitConnection()
        {
            var x = _hubConnectionBuilder
                .WithUrlBlazor(new Uri("/unityremotecontrolhub", UriKind.Relative), JsRuntime,
                options: opt =>
                {
                    opt.UseServerSide(UriHelper);
                    //opt.SkipNegotiation = true;
                    opt.AccessTokenProvider = async () =>
                    {
                        var token = await GetJwtToken("DemoUser");
                        return token;
                    };
                });
            Connection = x
                .Build();

            //only register for logging purposes
            Connection.On<string>("Command", PrintCommand);

            /// Der Webclient muss die Animationen auflisten können.
            Connection.On<string>("AddAnimations", animations =>
            {
                foreach (var animation in animations.Split(','))
                {
                    Animations.Add(animation);
                }
                StateHasChanged();
                return Task.CompletedTask;
            });
            
            Connection.Closed += exc =>
            {
                if (exc != null)
                {
                    Logger.LogError(exc, "Connection was closed!");
                }
                return Task.CompletedTask;
            };

            await Connection.StartAsync();
        }

        /* [Inject] */
        private ILogger<UnityRemoteControlComponentBase> Logger { get; set; } = new ILogger<UnityRemoteControlComponentBase>(); 

        private async Task<string> GetJwtToken(string userId)
        {
            var httpResponse = await Http.GetAsync($"/generatetoken?user={userId}");
            httpResponse.EnsureSuccessStatusCode();
            return await httpResponse.Content.ReadAsStringAsync();
        }
        
        /// <summary>
        /// payload für die unregistrierten Befehle
        /// </summary>
        internal string CommandText { get; set; }

        /// <summary>
        /// Festlegen der Farbe
        /// </summary>
        internal string Color {
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

        protected override async Task OnInitAsync()
        {
            Logger.LogMessages = LogOutput;
            InitConnection();
        }

        //protected override Task OnAfterRenderAsync()
        //{
        //    return base.OnAfterRenderAsync().ContinueWith(t =>
        //    {
        //        if (Connection is null)
        //        {
        //            InitConnection();
        //        }
        //    });
        //}

        private Task PrintCommand(object msg)
        {
            Logger.LogInformation(msg);
            StateHasChanged(); 
            return Task.CompletedTask;
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
}
