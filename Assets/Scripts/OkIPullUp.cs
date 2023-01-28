using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OkIPullUp : MonoBehaviour
{
    public void PlaySound()
    {
        XRSocketInteractor socket = GetComponent<XRSocketInteractor>();
        IXRSelectInteractable currentItem = socket.GetOldestInteractableSelected();
        Destroy(currentItem.transform.gameObject);
        GetComponent<AudioSource>().Play();
    }
}
