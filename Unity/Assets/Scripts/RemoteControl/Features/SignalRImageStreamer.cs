namespace Assets.Scripts.Features
{
    public class SignalRImageStreamer : SignalRImageStreamerBase<SignalRCommandEntityController, (string Command, string Value)>
    {
        public override void OnInitFeature(object sender, EventArgs<(string Command, string Value)> args)
        {
        }
    }
}
