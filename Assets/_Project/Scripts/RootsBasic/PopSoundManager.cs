using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PopSoundManager : MonoBehaviour
{
    public static PopSoundManager _instance;
    public float playCooldown;
    AudioSource audioSource;
    bool isPlaying = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        _instance = this;
    }

    public void Play()
    {
        if (!isPlaying)
        {
            audioSource.Play();
            isPlaying = true;
            StartCoroutine(PlayCooldown());
        }

        IEnumerator PlayCooldown()
        {
            yield return new WaitForSeconds(playCooldown);
            isPlaying = false;
        }
    }
}
