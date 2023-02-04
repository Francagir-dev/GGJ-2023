using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicRoot : MonoBehaviour, IRoot
{
    public Transform weedsVisual;
    public float scaleMult = 5f;
    public float pullForceRequired = 3;
    UnityEvent onPulledRoot;
    Transform grabSource;
    Vector3 formerPullingPoint;
    Quaternion originalRot;

    public int pullsRequired;
    AudioSource audioSource;

    public UnityEvent OnPulledRoot { get => onPulledRoot; set => onPulledRoot = value; }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        onPulledRoot = new UnityEvent();
        originalRot = weedsVisual.localRotation;
    }

    private void Update()
    {
        if (weedsVisual != null)
        {
            WeedBehaviour();
        }
    }

    private void WeedBehaviour()
    {
        if (grabSource != null)
        {
            weedsVisual.LookAt(grabSource, Vector3.up);
            weedsVisual.localScale = (Vector3.one * 8.2655f) + Vector3.forward * Vector3.Distance(transform.position, grabSource.position) * scaleMult;

            if (Vector3.Distance(formerPullingPoint, grabSource.position) > pullForceRequired)
            {
                OnPull();
            }
        }
        else
        {
            weedsVisual.localScale = Vector3.one * 8.2655f;
            weedsVisual.localRotation = originalRot;
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
        audioSource.Play();
        Destroy(weedsVisual.gameObject);
        weedsVisual = null;
        Destroy(gameObject, 5);
    }
}
