using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityTv : MonoBehaviour
{
    public GameObject screen;
    public Material[] liveFeeds;
    public Material screenOff;
    int currentVideo = 0;
    bool isOn = false;

    public void Toggle()
    {
        if (!isOn)
        {
            GameObject.Find(liveFeeds[currentVideo].name).GetComponentInChildren<Camera>().enabled = true;
            screen.GetComponent<MeshRenderer>().material = liveFeeds[currentVideo];
            isOn = true;
        }
        else
        {
            GameObject.Find(liveFeeds[currentVideo].name).GetComponentInChildren<Camera>().enabled = false;
            screen.GetComponent<MeshRenderer>().material = screenOff;
            isOn = false;
        }
    }

    public void LeftVideo()
    {
        if (isOn)
        {
            GameObject.Find(liveFeeds[currentVideo].name).GetComponentInChildren<Camera>().enabled = false;
            if (currentVideo == 0)
            {
                currentVideo = liveFeeds.Length - 1;
                GameObject.Find(liveFeeds[currentVideo].name).GetComponentInChildren<Camera>().enabled = true;
                screen.GetComponent<MeshRenderer>().material = liveFeeds[currentVideo];
            }
            else
            {
                currentVideo -= 1;
                GameObject.Find(liveFeeds[currentVideo].name).GetComponentInChildren<Camera>().enabled = true;
                screen.GetComponent<MeshRenderer>().material = liveFeeds[currentVideo];
            }
        }
    }

    public void RightVideo()
    {
        if (isOn)
        {
            GameObject.Find(liveFeeds[currentVideo].name).GetComponentInChildren<Camera>().enabled = false;
            if (currentVideo == liveFeeds.Length - 1)
            {
                currentVideo = 0;
                GameObject.Find(liveFeeds[currentVideo].name).GetComponentInChildren<Camera>().enabled = true;
                screen.GetComponent<MeshRenderer>().material = liveFeeds[currentVideo];
            }
            else
            {
                currentVideo += 1;
                GameObject.Find(liveFeeds[currentVideo].name).GetComponentInChildren<Camera>().enabled = true;
                screen.GetComponent<MeshRenderer>().material = liveFeeds[currentVideo];
            }
        }
    }
}
