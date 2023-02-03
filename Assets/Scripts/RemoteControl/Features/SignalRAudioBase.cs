using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

/// <summary>
/// Generische abstrakte Basisklasse für das Abspielen von Audioquellen
/// </summary>
/// <typeparam name="TEntityController"></typeparam>
/// <typeparam name="TArgs"></typeparam>
public abstract class SignalRAudioBase<TEntityController, TArgs> : SignalRUnityFeatureBase<TEntityController, TArgs>
    where TEntityController : SignalREntityController<TArgs>
{
    /// <summary>
    /// Audioquelle, von der aus Töne gespielt werden.
    /// </summary>
    public AudioSource audioSource;

    /// <summary>
    /// Gibt die abgespielte Datei an.
    /// </summary>
    public string SoundFile
    {
        get => _SoundFile;
        set
        {
            if (_SoundFile != value)
            {
                _SoundFile = value;
                PlayAudio(_SoundFile);
            }
        }
    }
    private string _SoundFile;

    /// <summary>
    /// Stummschalten der Audioquelle
    /// </summary>
    public bool Muted
    {
        get => _Muted;
        set
        {
            if (_Muted != value)
            {
                UnityMainThreadDispatcher.Instance().Enqueue(() => audioSource.mute = value);
                _Muted = value;
            }
        }
    }
    private bool _Muted;

    /// <summary>
    /// Mit diesen Dateiendungen können wir umgehen.
    /// </summary>
    private HashSet<string> ValidFileExtensions = new HashSet<string>
    {
        ".wav",
        ".ogg"
    };

    /// <summary>
    /// Abspielen von Audiodateien
    /// </summary>
    /// <param name="url"></param>
    private void PlayAudio(string url)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            if (File.Exists(url))
            {
                url = "file://" + url;
            }
            //UnityWebRequest uwr = new UnityWebRequest(link);
            //var data = uwr.downloadHandler.data;
            //AudioClip clip2 = AudioClip.Create( new AudioClip();
            else if (!url.StartsWith("http")) // HACK - rausbekommen, ob das wirklich ein relativer URL ist.
            {
                url = entityController.entity.clientController.connectionManager.UsedSignalRServer + "/" + url;
            }
            if (TryGetAudioClip(url, out var audioClip))
            {
                audioSource.clip = audioClip;
                audioSource.Play();
            }
        });
    }


    private bool TryGetAudioClip(string url, out AudioClip audioClip)
    {
        var enumerator = GetAudioClip(url);
        audioClip = null;
        while(enumerator.MoveNext())
        {
            audioClip = enumerator.Current as AudioClip;
        }
        return audioClip != null;
    }

    private IEnumerator GetAudioClip(string url)
    {
        using UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.OGGVORBIS);
        yield return www.SendWebRequest();

        while(www.result == UnityWebRequest.Result.InProgress)
        {
            //Debug.Log("returning null");

            yield return null;
        }
        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(www.error);
        }
        else if (www.result == UnityWebRequest.Result.Success)
        {
            AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
            yield return myClip;
        }
        else
        {
            Debug.Log($"weird case result: {www.result} error: {www.error}");
        }
    }

    public void HandleAudioCommand(string audioCommand)
    {
        switch (audioCommand)
        {
            case "stop": StopAudio(); break;
            case "mute": Muted = true; break;
            case "unmute": Muted = false; break;
            default: PlayAudio(audioCommand); break;
        }
    }

    private void StopAudio()
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            audioSource.Stop();
        });
    }
}
