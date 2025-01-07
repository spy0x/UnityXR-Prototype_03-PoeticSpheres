using System;
using Oculus.Interaction;
using UnityEngine;

public class PoemPlayer : MonoBehaviour
{
    [SerializeField] SnapInteractable snapZone;
    [SerializeField] AudioSource audioSource;
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
        currentPoeticSphere = snapInteractor.GetComponentInParent<PoeticSphere>();
        if (!audioSource || !currentPoeticSphere) return;
        audioSource.clip = currentPoeticSphere.CurrentPoem.AudioClip;
        audioSource.Play();
    }
    private void StopPoem(SnapInteractor obj)
    {
        audioSource.Stop();
        audioSource.clip = null;
        currentPoeticSphere = null;
    }
    
    public bool HasPoem => currentPoeticSphere != null;
}