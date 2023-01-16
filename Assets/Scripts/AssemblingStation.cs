using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblingStation : MonoBehaviour
{
    public GameObject station;
    Vector3 startPosition;
    AudioSource sound;
    bool isPlaying = false;
    bool movingDown = false;
    bool movingUp = false;
    public float speed = 0.5f;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
        startPosition = station.transform.position;
    }

    public void startAnimation()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            movingDown = true;
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
