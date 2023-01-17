using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using SharedLib;

namespace AdminPanelBlazorServer.Components;

public class HubConnector
{
    public HubConnection Connection { get; set; }


    [Inject]
    private HttpClient Http { get; set; }

    private LoggerStub<HubConnector> Logger { get; set; } = new LoggerStub<HubConnector>();


    private async Task<string> GetJwtToken(string userId)
    {
        var httpResponse = await Http.GetAsync($"/generatetoken?user={userId}");
        httpResponse.EnsureSuccessStatusCode();
        return await httpResponse.Content.ReadAsStringAsync();
    }

    public HubConnector()
    {
        Connection = new HubConnectionBuilder()
            //.WithUrl(Navigation.ToAbsoluteUri("/unityremotecontrolhub"))

            .WithUrl("http://localhost:5000/unityremotecontrolhub")
            //, opt =>
            //{
            //    Logger.LogInformation($"Setting options");

            //    //opt.LogLevel = SignalRLogLevel.Debug;
            //    //opt.Transport = HttpTransportType.WebSockets;
            //    opt.SkipNegotiation = true;
            //    opt.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
            //    opt.AccessTokenProvider = async () =>
            //    {
            //        var token = await GetJwtToken("DemoUser");
            //        Logger.LogInformation($"Access Token: {token}");
            //        return token;
            //    };
            //})
            //.AddMessagePackProtocol()
            .Build();

        Connection.On<string>("Command", PrintCommand);



        Connection.Closed += async exc => Logger.LogError(exc, "Connection was closed!");
    }



    public void AddHandler<T>(string name, Action<T> handlerAction) => Connection.On<T>(name, x =>
    {
        handlerAction(x);
        return Task.CompletedTask;
    });


    private async Task PrintCommand(object msg)
    {
        Logger.LogInformation(msg);
    }

    private async Task Print<T>(T content)
    {
        Logger.LogInformation(content);
    }
}
