
public class SignalRCommandAnimationController : SignalRAnimationBase<SignalRCommandEntityController, (string Command, string Value)>
{
    public override void OnInitFeature(object sender, EventArgs<(string Command, string Value)> args)
    {
        if (args.Data.Command == "Animation")
        {
            PlayAnimation(args.Data.Value);
        }
    }
}