using UnityEngine;
using Microsoft.AspNetCore.SignalR.Client;

/// <summary>
/// Abstrakte Basisklasse für die unity-spezifischen Aspekte eines Features.
/// </summary>
public abstract class SignalRUnityFeatureBase : MonoBehaviour
{
    public void Awake() => AwakeVirtual();
    public virtual void AwakeVirtual() { }

    /// <summary>
    /// Benachrichtigung, dass der SignalR-Hub verbunden ist
    /// </summary>
    /// <param name="hubConnection"></param>
    public virtual void Connected(HubConnection hubConnection) { }

    public virtual void Disconnect() { }
}

/// <summary>
/// Generische abstrakte Basisklasse für die unity-spezifischen Aspekte eines Features.
/// </summary>
/// <typeparam name="TEntityController"></typeparam>
/// <typeparam name="TInitFeatureArgs"></typeparam>
public abstract class SignalRUnityFeatureBase<TEntityController, TInitFeatureArgs> : SignalRUnityFeatureBase
    where TEntityController : SignalREntityController<TInitFeatureArgs>
{
    public abstract void OnInitFeature(object sender, EventArgs<TInitFeatureArgs> args);

    public override void AwakeVirtual()
    {
        base.AwakeVirtual();
        InitFeature();
        entityController.FeatureControllers.Add(this);
    }

    /// <summary>
    /// Hier werden die Registrierungs-Events des Controllers angekabelt.
    /// </summary>
    public void InitFeature() => entityController.Init(OnInitFeature);

    public TEntityController entityController;
}
