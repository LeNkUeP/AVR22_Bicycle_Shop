using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using UnityEngine;

public abstract class SignalRAnimationBase<TEntityController, TArgs> : SignalRUnityFeatureBase<TEntityController, TArgs>
    where TEntityController : SignalREntityController<TArgs>
{
    public Animator animator;

    private readonly HashSet<string> AnimationNames = new HashSet<string>();

    public override void AwakeVirtual()
    {
        base.AwakeVirtual();
        foreach (var animationName in animator.runtimeAnimatorController.animationClips.Select(ac => ac.name))
        {
            AnimationNames.Add(animationName);
        }
    }

    public bool PlayAnimation(string animation)
    {
        if (AnimationNames.Contains(animation))
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() => animator.Play(animation));
            return true;
        }
        else
        {
            Debug.LogError("got an animation command with an unknown animationName: " + animation);
            return false;
        }
    }

    public override void Connected(HubConnection hubConnection)
    {
        base.Connected(hubConnection);
        hubConnection.SendAsync("AddAnimations", string.Join(",", AnimationNames));
    }

    public string DefaultAnimation = "Idle";
}