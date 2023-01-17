public class SignalRCommandAudioController : SignalRAudioBase<SignalRCommandEntityController, (string Command, string Value)>
{
    public override void OnInitFeature(object sender, EventArgs<(string Command, string Value)> args)
    {
        if (args.Data.Command == "Audio")
        {
            HandleAudioCommand(args.Data.Value);
        }
    }
}
