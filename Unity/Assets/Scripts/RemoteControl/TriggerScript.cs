using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TriggerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var audioSource = gameObject.GetComponent<AudioSource>();

            AudioClip clip = null;
            string link = "http://localhost:64987/files/posted/04d5cd42-2e6c-4e5c-8724-37d92eb40cce.mp3.ogg";
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                //UnityWebRequest uwr = new UnityWebRequest(link);
                //var data = uwr.downloadHandler.data;
                //AudioClip clip2 = AudioClip.Create( new AudioClip();

                WWW www1 = new WWW(link);
                clip = www1.GetAudioClip(false, true, AudioType.OGGVORBIS);
                //if (clip.isReadyToPlay)
                {
                    audioSource.clip = clip;
                    audioSource.Play();
                }
            });

            //bool isReadyToPlay = false;


            //while (clip != null && !isReadyToPlay)
            //{
            //    UnityMainThreadDispatcher.Instance().Enqueue(() =>
            //    {
            //        isReadyToPlay = clip.isReadyToPlay;
            //    });

            //    if (isReadyToPlay)
            //    {
            //        UnityMainThreadDispatcher.Instance().Enqueue(() =>
            //        {
            //            audioSource.clip = clip;
            //            audioSource.Play();
            //        });
            //    }
            //}

        }
    }
}
