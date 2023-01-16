using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace AdminPanelBlazorServer.Components;

public class HubConnectedComponent : ComponentBase
{
    [Inject]
    public HubConnector HubConnector { get; set; }

    protected HubConnection Connection { get; private set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        Connection = HubConnector.Connection;
    }
}
