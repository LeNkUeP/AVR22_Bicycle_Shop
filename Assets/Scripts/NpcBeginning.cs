using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBeginning : MonoBehaviour
{
    public NPCSpawner spawner;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.GetComponentInChildren<Animator>().gameObject);
        spawner.SpawnNPC();
    }
}
