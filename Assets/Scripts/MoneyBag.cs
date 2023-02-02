using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBag : MonoBehaviour
{
    public int rotateSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(transform.gameObject);
        other.GetComponentInChildren<Animator>().Play(other.GetComponent<NPC>().idleAnim);
    }
}
