using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicRoot : MonoBehaviour, IRoot
{
    public static float poisonEarth = 0;
    public static float pullStrenght = 1;
    public static float instaKillChance = 0;

    public Transform weedsVisual;
    public float scaleMult = 5f;
    public float pullForceRequired = 3;
    public float spawnTime = .3f;
    UnityEvent onPulledRoot;
    Transform grabSource;
    Vector3 formerPullingPoint;
    Quaternion originalRot;

    public float pullsRequired;
    public ParticleSystem leafParticle;

    public UnityEvent OnPulledRoot { get => onPulledRoot; set => onPulledRoot = value; }

    private void Start()
    {
        onPulledRoot = new UnityEvent();
        originalRot = weedsVisual.localRotation;
        SpawnRoot();
    }

    public void SpawnRoot()
    {
        transform.localScale = Vector3.zero;
        transform.Rotate(Vector3.forward * Random.Range(0, 360));
        StartCoroutine(RootSpawn());
        IEnumerator RootSpawn()
        {
            float timer = spawnTime;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, timer / spawnTime);
                yield return null;
            }
        }
    }

    private void Update()
    {
        pullsRequired -= Time.deltaTime * poisonEarth;
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
            weedsVisual.localScale = Vector3.one + Vector3.forward * Vector3.Distance(transform.position, grabSource.position) * scaleMult;

            if (Vector3.Distance(formerPullingPoint, grabSource.position) > pullForceRequired)
            {
                OnPull();
            }
        }
        else
        {
            weedsVisual.localScale = Vector3.one;
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
        pullsRequired -= 1 * pullStrenght;
        ResetPullingPoint();
        if (pullsRequired <= 0 || instaKillChance < Random.Range(0f, 100f))
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
        PopSoundManager._instance.Play();
        Destroy(weedsVisual.gameObject);
        Destroy(GetComponent<Collider>());
        leafParticle.Play();
        weedsVisual = null;
        Destroy(gameObject, 5);
    }
}
