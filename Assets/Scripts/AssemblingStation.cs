using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AssemblingStation : MonoBehaviour
{
    public GameObject station;
    Vector3 startPosition;
    AudioSource sound;
    bool isPlaying = false;
    bool movingDown = false;
    bool movingUp = false;
    float audioTime = 1;

    public bool onlyFullBike = true;
    public float speed = 0.5f;

    XRSocketInteractor backwheel;
    XRSocketInteractor frontwheel;
    XRSocketInteractor handlebar;
    XRSocketInteractor frame;
    XRSocketInteractor cylinder;
    XRSocketInteractor treadleleft;
    XRSocketInteractor treadleRight;
    XRSocketInteractor treadleCircleLeft;
    XRSocketInteractor treadleCircleRight;

    public XRSocketInteractor[] sockets;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
        startPosition = station.transform.position;
    }

    bool isBikeCompleted()
    {
        if (onlyFullBike)
        {
            foreach (XRSocketInteractor socket in sockets)
            {
                if (!socket.hasSelection)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void startAnimation()
    {
        if (!isPlaying && isBikeCompleted())
        {
            isPlaying = true;
            movingDown = true;
            sound.time = audioTime;
            sound.Play();
        }
    }

    void Update()
    {
        if (isPlaying)
        {
            if (movingDown)
            {
                if (transform.position.y > 2)
                {
                    transform.Translate(Vector3.down * speed * Time.deltaTime);
                }
                else
                {
                    movingDown = false;
                    movingUp = true;
                }
            }

            if (movingUp)
            {
                if (transform.position.y < startPosition.y)
                {
                    transform.Translate(Vector3.up * speed * Time.deltaTime);
                }
                else
                {
                    movingUp = false;
                    isPlaying = false;
                    sound.Stop();
                }
            }
        }
    }
}
