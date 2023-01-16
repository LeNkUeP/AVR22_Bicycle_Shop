using Microsoft.AspNetCore.SignalR.Client;

public class SignalRRegisteredCommandColorController : SignalRColorBase<SignalRRegisteredCommandEntityController, HubConnection>
{
    public override void OnInitFeature(object sender, EventArgs<HubConnection> args)
    {
        args.Data.On<string>("Color", HandleColor);
    }
}
