using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class SignalRImagerBase<TEntityController, TArgs> : SignalRUnityFeatureBase<TEntityController, TArgs>
    where TEntityController : SignalREntityController<TArgs>
{
    public RawImage image;
    private Texture2D imageTexture;
    public int textureWidth = 400;
    public int textureHeight = 400;

    public override void AwakeVirtual()
    {
        base.AwakeVirtual();
        imageTexture = new(300, 300);
    }
    public bool SetImageSource(string imageSource)
    {
        var bytes = Convert.FromBase64String(imageSource);
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            imageTexture.LoadImage(bytes);
            //Sprite sp = Sprite.Create(imageTexture, new(0, 0, 300, 300), new());
            //image.material.mainTexture = imageTexture;
            image.texture = imageTexture;
        });
        return true;
    }
}
