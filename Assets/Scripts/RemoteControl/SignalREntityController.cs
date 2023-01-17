using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using UnityEngine;

public abstract class SignalREntityController : MonoBehaviour
{
    #region public fields for unity

    /// <summary>Soll Fernsteuerung aktiviert werden?</summary>
    public bool remoteControlEnabled = true;

    public SignalREntity entity;

    #endregion

    public List<SignalRUnityFeatureBase> FeatureControllers { get; } = new List<SignalRUnityFeatureBase>();

    void Awake() => AwakeVirtual();

    protected virtual void AwakeVirtual() => entity.clientController.EntityControllers.Add(this);

    public virtual void RegisterCommands(HubConnection hubConnection) { }

    internal void Connected(HubConnection hubConnection)
    {
        foreach (var featureController in FeatureControllers)
        {
            featureController.Connected(hubConnection);
        }
    }

    internal void Disconnect()
    {
        foreach (var featureController in FeatureControllers)
        {
            featureController.Disconnect();
        }
    }
}

public abstract class SignalREntityController<TRegisterEventArgs> : SignalREntityController
{
    public abstract void Init(Action<object, EventArgs<TRegisterEventArgs>> onInitEvent);
}
