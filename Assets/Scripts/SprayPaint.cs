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

    float sprayLength = 1.2f;
    bool currentlyPainting = false;

    // Update is called once per frame
    void Update()
    {
        if (currentlyPainting)
        {
            if (Physics.Raycast(rayPoint.position, transform.TransformDirection(new Vector3(-1, 0, 0)), out RaycastHit hitinfo, sprayLength, layermask))
            {
                //Debug.Log("Hit something");
                GameObject target = hitinfo.collider.transform.gameObject;
                if (target.name.Equals("frame"))
                {
                    setParentSpecificMaterial(target,0);
                }
                else if (target.name.Equals("framedetails"))
                {
                    setParentSpecificMaterial(target, 1);
                }
                else if (target.name.Equals("seat"))
                {
                    setParentSpecificMaterial(target, 2);
                }
                else if (target.name.Equals("handle"))
                {
                    setParentSpecificMaterial(target, 3);
                }
                else if (target.name.Equals("fork"))
                {
                    setParentSpecificMaterial(target, 0);
                    setParentSpecificMaterial(target, 1);
                }
                else if (target.name.Equals("stem"))
                {
                    setParentSpecificMaterial(target, 2);
                }
                else
                {
                    target.GetComponent<Renderer>().material = color;
                }
                //Debug.DrawRay(rayPoint.position, transform.TransformDirection(new Vector3(-1, 0, 0)) * 20f, Color.red);
            }
            else
            {
                //Debug.Log("Hit nothing");
                //Debug.DrawRay(rayPoint.position, transform.TransformDirection(new Vector3(-1, 0, 0)) * 20f, Color.blue);
            }
        }
    }

    public void setParentSpecificMaterial(GameObject target, int index)
    {
        MeshRenderer parentRenderer = target.transform.parent.GetComponent<MeshRenderer>();

        var mats = parentRenderer.materials;
        mats[index] = color;
        parentRenderer.materials = mats;
    }

    public void Paint()
    {
        audioSource.enabled = true;
        particles.gameObject.SetActive(true);
        audioSource.time = 0.1f;
        audioSource.Play();
        particles.Play();
        currentlyPainting = true;
    }

    public void StopPaint()
    { 
        audioSource.Stop();
        particles.Stop();
        audioSource.enabled = false;
        currentlyPainting = false;
    }
}
