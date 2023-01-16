public class SignalRCommandColorController : SignalRColorBase<SignalRCommandEntityController, (string Command, string Value)>
{
    public override void OnInitFeature(object sender, EventArgs<(string Command, string Value)> args)
    {
        if (args.Data.Command == "Color")
        {
            HandleColor(args.Data.Value);
        }
    }
}