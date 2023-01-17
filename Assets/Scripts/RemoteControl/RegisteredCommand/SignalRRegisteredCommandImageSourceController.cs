using Microsoft.AspNetCore.SignalR.Client;

public class SignalRRegisteredCommandImageSourceController : SignalRImagerBase<SignalRRegisteredCommandEntityController, HubConnection>
{
    public override void OnInitFeature(object sender, EventArgs<HubConnection> args)
    {
        args.Data.On<string>("CompanionAppScreenshot", imgSrc => SetImageSource(imgSrc));
    }
}