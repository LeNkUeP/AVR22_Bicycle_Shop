using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SecurityCam : MonoBehaviour
{
    public GameObject kinematicPart;
    public GameObject pivot;
    public Camera cam;
    public RenderTexture renderTexture;

    bool startNextRotation = true;
    bool rotateRight;

    public float radius;
    public float pitch;

    public float rotateTime;
    public float holdingTime;

    private void Start()
    {
        if (renderTexture != null)
        {
            cam.targetTexture = renderTexture;
        }
        pivot.transform.localRotation = Quaternion.AngleAxis(pitch, Vector3.right);
        SetUpStartRotation();
    }

    private void Update()
    {
        if (startNextRotation && rotateRight)
        {
            StartCoroutine(Rotate(radius, rotateTime));
        }else if (startNextRotation && !rotateRight)
        {
            StartCoroutine(Rotate(-radius, rotateTime));
        }
    }

    IEnumerator Rotate(float radius, float duration)
    {
        startNextRotation = false;

        Quaternion initialRotation = kinematicPart.transform.rotation;

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            kinematicPart.transform.rotation = initialRotation * Quaternion.AngleAxis(timer / duration * radius, Vector3.forward);
            yield return null;
        }

        yield return new WaitForSeconds(holdingTime);

        startNextRotation = true;
        rotateRight = !rotateRight;
    }

    void SetUpStartRotation()
    {
        if (rotateRight)
        {
            kinematicPart.transform.localRotation = Quaternion.AngleAxis(-radius/2, Vector3.forward);
        }
        else
        {
            kinematicPart.transform.localRotation = Quaternion.AngleAxis(radius / 2, Vector3.forward);
        }
    }
}
