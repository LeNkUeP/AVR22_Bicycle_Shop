using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject[] humans;
    public GameObject[] zombies;
    public RuntimeAnimatorController controller;
    public string[] animations;
    public float[] speeds;

    private NPC npc;
    private GameObject randomNpc;

    // Start is called before the first frame update
    void Start()
    {
        npc = transform.GetChild(0).GetComponent<NPC>();
        SpawnNPC();
    }

    public void SpawnNPC()
    {
        npc.initDestination();
        int r = Random.Range(1, 5);

        // Zombie
        if (r == 1)
        {
            randomNpc = Instantiate(zombies[Random.Range(0, zombies.Length)]);
            randomNpc.GetComponent<Animator>().runtimeAnimatorController = controller;
            npc.idleAnim = "Zombie idle";
            npc.walkingAnim = "Zombie walking";
            randomNpc.GetComponent<Animator>().Play(npc.walkingAnim);
            npc.agent.speed = 2f;
        }
        // Human
        else
        {
            randomNpc = Instantiate(humans[Random.Range(0, humans.Length)]);
            randomNpc.GetComponent<Animator>().runtimeAnimatorController = controller;
            int randomAnim = Random.Range(0, animations.Length);
            npc.idleAnim = animations[randomAnim] + " idle";
            npc.walkingAnim = animations[randomAnim] + " walking";
            randomNpc.GetComponent<Animator>().Play(npc.walkingAnim);
            npc.agent.speed = speeds[randomAnim];
        }
        randomNpc.transform.parent = transform.GetChild(0).transform;
        randomNpc.transform.localPosition = new Vector3(0, -0.08f, 0);
        randomNpc.transform.localRotation = Quaternion.identity;
        randomNpc.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    }
}
