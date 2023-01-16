using UnityRemoteControl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace UnityRemoteControl.Hubs
{
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    public class UnityRemoteControlHub : Hub
    {
        #region Connect / Disconnect

        public override async Task OnConnectedAsync() => await Clients.All.SendAsync("Connected", $"{Context.ConnectionId}");

        public override async Task OnDisconnectedAsync(Exception ex) => await Clients.Others.SendAsync("Disconnected", $"{Context.ConnectionId}");

        #endregion

        /// <summary>
        /// Für nichtregistrierte Befehle
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task Command(string message) => Clients.All.SendAsync("Command", Context.ConnectionId, Context.UserIdentifier, message);

        /// <summary>
        /// Registrierter Befehl "Color"
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public Task Color(string color) => Clients.All.SendAsync("Color", color);

        /// <summary>
        /// Registrierter Befehl "Audio"
        /// </summary>
        /// <param name="audio"></param>
        /// <returns></returns>
        public Task Audio(string audio) => Clients.All.SendAsync("Audio", audio);

        /// <summary>
        /// Registrierter Befehl "Animation"
        /// </summary>
        /// <param name="animation"></param>
        /// <returns></returns>
        public Task Animation(string animation) => Clients.All.SendAsync("Animation", animation);

        /// <summary>
        /// Animationen hinzufügen
        /// </summary>
        /// <param name="animations"></param>
        /// <returns></returns>
        public Task AddAnimations(string animations) => Clients.All.SendAsync("AddAnimations", animations);
    }
}
