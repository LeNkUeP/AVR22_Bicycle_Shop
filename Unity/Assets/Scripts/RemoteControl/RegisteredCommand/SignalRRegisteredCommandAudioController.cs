using Microsoft.AspNetCore.SignalR.Client;

public class SignalRRegisteredCommandAudioController : SignalRAudioBase<SignalRRegisteredCommandEntityController, HubConnection>
{
    public override void OnInitFeature(object sender, EventArgs<HubConnection> args)
    {
        args.Data.On<string>("Audio", audio => HandleAudioCommand(audio));
    }
}