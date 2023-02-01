using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NewXRGrabInteractable : XRGrabInteractable
{

    [SerializeField]
    bool unlimited = false;


    /// <summary>
    /// Updates the state of the object due to being grabbed.
    /// Automatically called when entering the Select state.
    /// </summary>
    /// <seealso cref="Drop"/>
    protected override void Grab()
    {
        if (unlimited)
        {
            GameObject duplicate = Instantiate(gameObject, transform.position, transform.rotation);
            duplicate.GetComponent<Rigidbody>().isKinematic = true;
            unlimited = false;
            GetComponent<Rigidbody>().isKinematic = false;
            duplicate.transform.parent = transform.parent;
        }
        base.Grab();
    }

}