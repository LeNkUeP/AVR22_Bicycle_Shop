using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class FlickeringLight : MonoBehaviour
{
    public Light led;
    public float blinkingSpeed;

    private void Start()
    {
        InvokeRepeating("Flicker", blinkingSpeed, blinkingSpeed);
    }

    void Flicker()
    {
        led.enabled = !led.enabled;
    }
}
