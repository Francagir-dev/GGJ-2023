using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRoot : MonoBehaviour, IRoot
{
    public float scaleMult = 1.2f;
    Transform grabSource;
    Quaternion originalRot;

    private void Start()
    {
        originalRot = transform.localRotation;
    }

    private void Update()
    {
        if (grabSource != null)
        {
            transform.LookAt(grabSource, Vector3.up);
            transform.localScale = (Vector3.one * 8.2655f) + Vector3.forward * Vector3.Distance(transform.position, grabSource.position) * scaleMult;
        }
        else 
        {
            transform.localScale = Vector3.one * 8.2655f;
            transform.localRotation = originalRot; 
        }
    }

    public void onGrab(Transform grabSource)
    {
        this.grabSource = grabSource;
    }

    public void onPull()
    {
        throw new System.NotImplementedException();
    }

    public void onRelease()
    {
        grabSource = null;
    }
}
