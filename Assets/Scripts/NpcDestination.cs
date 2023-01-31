using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDestination : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponentInChildren<Animator>().Play(other.GetComponent<NPC>().idleAnim);
    }
}
