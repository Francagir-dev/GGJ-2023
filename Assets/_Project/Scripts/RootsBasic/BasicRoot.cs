using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicRoot : MonoBehaviour, IRoot
{
    public float scaleMult = 5f;
    public float pullForceRequired = 3;
    UnityEvent onPulledRoot;
    Transform grabSource;
    Vector3 formerPullingPoint;
    Quaternion originalRot;

    public int pullsRequired;

    public UnityEvent OnPulledRoot { get => onPulledRoot; set => onPulledRoot = value; }

    private void Start()
    {
        onPulledRoot = new UnityEvent();
        originalRot = transform.localRotation;
    }

    private void Update()
    {
        if (grabSource != null)
        {
            transform.LookAt(grabSource, Vector3.up);
            transform.localScale = (Vector3.one * 8.2655f) + Vector3.forward * Vector3.Distance(transform.position, grabSource.position) * scaleMult;

            if (Vector3.Distance(formerPullingPoint, grabSource.position) > pullForceRequired)
            {
                OnPull();
            }
        }
        else 
        {
            transform.localScale = Vector3.one * 8.2655f;
            transform.localRotation = originalRot; 
        }
    }

    public virtual void OnGrab(Transform grabSource)
    {
        this.grabSource = grabSource;
        ResetPullingPoint();
    }

    private void ResetPullingPoint()
    {
        formerPullingPoint = grabSource.position;
    }

    public virtual void OnPull()
    {
        pullsRequired--;
        ResetPullingPoint();
        if (pullsRequired <= 0)
        {
            RootPulled();
        }
    }

    public virtual void OnRelease()
    {
        onPulledRoot.RemoveAllListeners();
        grabSource = null;
    }

    public virtual void RootPulled()
    {
        onPulledRoot?.Invoke();
        Destroy(gameObject);
    }
}
