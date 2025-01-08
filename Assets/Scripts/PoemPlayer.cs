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



    private void OnEnable()
    {
        snapZone.WhenSelectingInteractorAdded.Action += PlayPoem;
        snapZone.WhenSelectingInteractorRemoved.Action += StopPoem;
    }



    private void OnDisable()
    {
        snapZone.WhenSelectingInteractorAdded.Action -= PlayPoem;
        snapZone.WhenSelectingInteractorRemoved.Action -= StopPoem;
    }

    public void OnSelect()
    {
        
    }



    public void OnDeselect()
    {
    }
    
    private void PlayPoem(SnapInteractor snapInteractor)
    {
        GameManager.Instance.StartPassthroughFadeToBlack(fadeDuration);
        currentPoeticSphere = snapInteractor.GetComponentInParent<PoeticSphere>();
        if (!audioSource || !currentPoeticSphere) return;
        audioSource.clip = currentPoeticSphere.CurrentPoem.AudioClip;
        audioSource.Play();
    }



    private void StopPoem(SnapInteractor obj)
    {
        GameManager.Instance.StartPassthroughFadeToOriginal(fadeDuration);
        audioSource.Stop();
        audioSource.clip = null;
        currentPoeticSphere = null;
    }



    public bool HasPoem => currentPoeticSphere != null;
}