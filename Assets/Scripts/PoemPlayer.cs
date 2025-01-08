using System;
using System.Collections;
using Oculus.Interaction;
using UnityEngine;

public class PoemPlayer : MonoBehaviour
{
    [SerializeField] SnapInteractable snapZone;
    [SerializeField] AudioSource audioSource;

    [SerializeField] float fadeDuration = 2f;
    private PoeticSphere currentPoeticSphere;

    private static PoemPlayer instance;
    public static PoemPlayer Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void OnEnable()
    {
        // snapZone.WhenSelectingInteractorAdded.Action += PlayPoem;
        // snapZone.WhenSelectingInteractorRemoved.Action += StopPoem;
    }



    private void OnDisable()
    {
        // snapZone.WhenSelectingInteractorAdded.Action -= PlayPoem;
        // snapZone.WhenSelectingInteractorRemoved.Action -= StopPoem;
    }
    
    public void PlayPoem(AudioClip audioClip)
    {
        GameManager.Instance.StartPassthroughFadeToBlack(fadeDuration);
        if (!audioSource) return;
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void StopPoem()
    {
        GameManager.Instance.StartPassthroughFadeToOriginal(fadeDuration);
        audioSource.Stop();
        audioSource.clip = null;
    }

    // public bool HasPoem => currentPoeticSphere != null;
    
}