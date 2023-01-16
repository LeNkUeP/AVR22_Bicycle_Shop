using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace VRProjectServer.Hubs;

//[Authorize(JwtBearerDefaults.AuthenticationScheme)]
public class UnityRemoteControlHub : Hub
{
    #region Connect / Disconnect

    public override async Task OnConnectedAsync() => await Clients.Others.SendAsync("Connected", $"{Context.ConnectionId}");

    public override async Task OnDisconnectedAsync(Exception ex) => await Clients.Others.SendAsync("Disconnected", $"{Context.ConnectionId}");

    #endregion

    /// <summary>
    /// Für nichtregistrierte Befehle
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task Command(string message) => await Clients.All.SendAsync(nameof(Command), Context.ConnectionId, Context.UserIdentifier, message);

    /// <summary>
    /// Registrierter Befehl "Color"
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public async Task Color(string color) => await Clients.Others.SendAsync(nameof(Color), color);

    /// <summary>
    /// Registrierter Befehl "Audio"
    /// </summary>
    /// <param name="audio"></param>
    /// <returns></returns>
    public async Task Audio(string audio) => await Clients.Others.SendAsync(nameof(Audio), audio);

    /// <summary>
    /// Registrierter Befehl "Animation"
    /// </summary>
    /// <param name="animation"></param>
    /// <returns></returns>
    public async Task Animation(string animation) => await Clients.Others.SendAsync(nameof(Animation), animation);

    /// <summary>
    /// Animationen hinzufügen
    /// </summary>
    /// <param name="animations"></param>
    /// <returns></returns>
    public async Task AddAnimations(string animations) => await Clients.Others.SendAsync(nameof(AddAnimations), animations);

    /// <summary>
    /// Animationen hinzufügen
    /// </summary>
    /// <param name="animations"></param>
    /// <returns></returns>
    public async Task GameplayImage(string imgBase64) => await Clients.Others.SendAsync(nameof(GameplayImage), imgBase64);

    public async Task CompanionAppScreenshot(string imgBase64) => await Clients.Others.SendAsync(nameof(CompanionAppScreenshot), imgBase64);
}
