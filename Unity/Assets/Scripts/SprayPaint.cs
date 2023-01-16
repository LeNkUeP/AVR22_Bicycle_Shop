using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayPaint : MonoBehaviour
{

    [SerializeField] LayerMask layermask;
    public Transform rayPoint;
    public Material color;
    public AudioSource audioSource;
    public ParticleSystem particles;

    bool currentlyPainting = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentlyPainting)
        {
            if (Physics.Raycast(rayPoint.position, transform.TransformDirection(new Vector3(-1, 0, 0)), out RaycastHit hitinfo, 20f, layermask))
            {
                //Debug.Log("Hit something");
                GameObject target = hitinfo.transform.gameObject;
                target.GetComponent<Renderer>().material = color;
                //Debug.DrawRay(rayPoint.position, transform.TransformDirection(new Vector3(-1, 0, 0)) * 20f, Color.red);
            }
            else
            {
                //Debug.Log("Hit nothing");
                //Debug.DrawRay(rayPoint.position, transform.TransformDirection(new Vector3(-1, 0, 0)) * 20f, Color.blue);
            }
        }
        else
        {
            // nothing
        }
    }

    public void paint()
    {
        audioSource.enabled = true;
        particles.gameObject.SetActive(true);
        audioSource.Play();
        particles.Play();
        currentlyPainting = true;
    }

    public void stopPaint()
    { 
        audioSource.Pause();
        particles.Pause();
        audioSource.enabled = false;
        particles.gameObject.SetActive(false);
        currentlyPainting = false;
    }
}
