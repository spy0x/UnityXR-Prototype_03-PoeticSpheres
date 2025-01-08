using System;
using System.Collections;
using Oculus.Interaction;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PoeticSphere : MonoBehaviour
{
    // [SerializeField] SnapInteractable snapZone;
    [SerializeField] GameObject[] orbPrefabs;
    [SerializeField] AudioTrigger grabAudioTrigger;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] GameObject label;
    [SerializeField] private TextMeshProUGUI labelText;
    [SerializeField] float delayReturn = 10f;
    [SerializeField] private float returnForce = 0.02f;
    [SerializeField] Rigidbody rb;

    private Poem currentPoem;
    private GameObject currentOrb;
    private Vector3 originalPosition;
    private float timer;
    public Poem CurrentPoem => currentPoem;
    private bool isReturning;

    void Start()
    {
        // if (snapZone) snapZone.transform.SetParent(null);
        originalPosition = transform.position;
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

    private void FixedUpdate()
    {
        if (isReturning) MoveToReturnPoint();
    }

    private void MoveToReturnPoint()
    {
        if (Vector3.Distance(transform.position, originalPosition) > 0.01f)
        {
            rb.AddForce((originalPosition - transform.position) * returnForce, ForceMode.Force);
        }
        else
        {
            rb.Sleep();
            isReturning = false;
        }
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
        string speaker = currentPoem.Speaker.Length > 0 ? $"\nSpeaker: {currentPoem.Speaker}" : "";
        // <size=125%><i>Do Not Go Gentle into That Good Night</i></size>\n\nBy DYLAN THOMAS\nSpeaker: Alfred Molina
        labelText.text =
            $"<size=125%><i>{currentPoem.Title}</i></size>\n\nBy {currentPoem.Author}{speaker}";
    }

    public void PlayGrabSound(PointerEvent pointerEvent)
    {
        if (audioSource.isPlaying) return;
        grabAudioTrigger.PlayAudio();
    }

    public void OnSelect()
    {
        StopAllCoroutines();
        isReturning = false;
    }

    public void OnDeselect()
    {
        StopAllCoroutines();
        isReturning = false;
        StartCoroutine(ReturnToOriginalPositionCoroutine());
    }

    private IEnumerator ReturnToOriginalPositionCoroutine()
    {
        yield return new WaitUntil(() => rb.linearVelocity.magnitude < 0.01f);
        yield return new WaitForSeconds(delayReturn);
        isReturning = true;
    }
}