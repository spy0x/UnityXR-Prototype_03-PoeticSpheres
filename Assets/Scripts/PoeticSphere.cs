using System;
using Oculus.Interaction;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PoeticSphere : MonoBehaviour
{
    [SerializeField] SnapInteractable snapZone;
    [SerializeField] GameObject[] orbPrefabs;
    [SerializeField] AudioTrigger grabAudioTrigger;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] GameObject label;
    [SerializeField] private TextMeshProUGUI labelText;

    private Poem currentPoem;
    private GameObject currentOrb;
    private Transform originalTransform;
    public Poem CurrentPoem => currentPoem;

    void Start()
    {
        if (snapZone) snapZone.transform.SetParent(null);
        currentPoem = GameManager.Instance.GetRandomPoem();
        // label.playerHead = GameManager.Instance.PlayerHead;
        SpawnRandomOrb();
        SetOrbText();
        label.SetActive(false);
    }

    private void Update()
    {
        LookTowardsPlayer();
    }

    private void LookTowardsPlayer()
    {
        if (!label) return;
        label.transform.LookAt(GameManager.Instance.PlayerHead);
    }

    private void SpawnRandomOrb()
    {
        if (orbPrefabs.Length == 0) return;
        var prefab = orbPrefabs[Random.Range(0, orbPrefabs.Length)];
        currentOrb = Instantiate(prefab, transform.position, Quaternion.identity, transform);
    }

    private void SetOrbText()
    {
        // <size=125%><i>Do Not Go Gentle into That Good Night</i></size>\n\nBy DYLAN THOMAS\nSpeaker: Alfred Molina
        labelText.text = $"<size=125%><i>{currentPoem.Title}</i></size>\n\nBy {currentPoem.Author}\nSpeaker: {currentPoem.Speaker}";
    }

    public void PlayGrabSound(PointerEvent pointerEvent)
    {
        if (audioSource.isPlaying) return;
        grabAudioTrigger.PlayAudio();
    }
}