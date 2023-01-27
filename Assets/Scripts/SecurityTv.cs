using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityTv : MonoBehaviour
{
    public GameObject screen;
    public Material[] liveFeeds;
    int currentVideo = 0;

    // Start is called before the first frame update
    void Start()
    {
        screen.GetComponent<MeshRenderer>().material = liveFeeds[currentVideo];
    }
    
    public void LeftVideo()
    {
        if (currentVideo == 0)
        {
            currentVideo = liveFeeds.Length - 1;
            screen.GetComponent<MeshRenderer>().material = liveFeeds[currentVideo];
        }
        else
        {
            currentVideo -= 1;
            screen.GetComponent<MeshRenderer>().material = liveFeeds[currentVideo];
        }
    }

    public void RightVideo()
    {
        if (currentVideo == liveFeeds.Length-1)
        {
            currentVideo = 0;
            screen.GetComponent<MeshRenderer>().material = liveFeeds[currentVideo];
        }
        else
        {
            currentVideo += 1;
            screen.GetComponent<MeshRenderer>().material = liveFeeds[currentVideo];
        }
    }
}
