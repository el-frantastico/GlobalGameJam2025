using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class OverworldMusic : MonoBehaviour
{
    static public OverworldMusic Instance { get; private set; }

    [Serializable]
    public struct VolumeAtBox
    {
        public GameObject TrigggerBox;
        public float HappyVolume;
        public float DarkVolume;
    }

    [SerializeField]
    private AudioSource happyAudioSource;

    [SerializeField]
    private AudioSource darkAudioSource;

    [SerializeField]
    private float volumeChangeDuration;

    [SerializeField]
    private float defaultHappyVolume = 1;

    [SerializeField]
    private float defaultDarkVolume = 0;

    [SerializeField]
    private VolumeAtBox[] volumeAtBoxes;

    private Coroutine happyCoroutine;
    private Coroutine darkCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        Reset();
    }

    public void Reset()
    {
        happyAudioSource.Stop();
        darkAudioSource.Stop();
        happyAudioSource.volume = defaultHappyVolume;
        darkAudioSource.volume = defaultDarkVolume;
        happyAudioSource.Play();
        darkAudioSource.Play();
    }

    public void EnterBox(GameObject box)
    {
        foreach (VolumeAtBox potentialBox in volumeAtBoxes)
        {
            if (box == potentialBox.TrigggerBox)
            {
                if(happyCoroutine != null)
                {
                    StopCoroutine(happyCoroutine);
                }
                if (darkCoroutine != null)
                {
                    StopCoroutine(darkCoroutine);
                }

                happyCoroutine = StartCoroutine(LerpVolume(happyAudioSource, potentialBox.HappyVolume, volumeChangeDuration));
                darkCoroutine = StartCoroutine(LerpVolume(darkAudioSource, potentialBox.DarkVolume, volumeChangeDuration));
                return;
            }
        }
    }

    IEnumerator LerpVolume(AudioSource audioSource, float targetVolume, float duration)
    {
        float elapsedTime = 0;
        float waitTime = duration;
        float currentVolume = audioSource.volume;

        while (elapsedTime < waitTime)
        {
            audioSource.volume = Mathf.Lerp(currentVolume, targetVolume, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
}
