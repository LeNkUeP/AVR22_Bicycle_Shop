using Microsoft.AspNetCore.SignalR.Client;

public class SignalRRegisteredCommandAnimationController : SignalRAnimationBase<SignalRRegisteredCommandEntityController, HubConnection>
{
    public override void OnInitFeature(object sender, EventArgs<HubConnection> args)
    {
        args.Data.On<string>("Animation", anim => PlayAnimation(anim));
    }
}
