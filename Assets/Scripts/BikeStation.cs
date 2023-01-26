using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BikeStation : MonoBehaviour
{
    GameObject bike;
    Vector3 startPosition;
    AudioSource sound;
    bool isPlaying = false;
    bool movingDown = false;
    bool movingUp = false;
    float audioTime = 1;

    public float speed = 0.5f;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        startPosition = transform.localPosition;
    }

    public bool IsEmpty(){
        if (transform.childCount == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void StartAnimation(GameObject assembledBike)
    {
        if (!isPlaying && assembledBike != null)
        {
            bike = assembledBike;
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
                if (transform.localPosition.y > 2)
                {
                    transform.Translate(Vector3.down * speed * Time.deltaTime);
                }
                else
                {
                    movingDown = false;
                    movingUp = true;
                    bike.transform.parent = transform;
                }
            }

            if (movingUp)
            {
                if (transform.localPosition.y < startPosition.y)
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
