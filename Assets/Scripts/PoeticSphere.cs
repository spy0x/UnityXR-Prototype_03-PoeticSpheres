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
    [SerializeField] private float scaleSizeFactor = 2f;
    [SerializeField] float returnScaleSpeed = 0.1f;
    [SerializeField] float labelMinDistance = 0.5f;

    private Poem currentPoem;
    private Vector3 originalPosition;
    private float timer;
    private static event Action onPoemStart;
    public Poem CurrentPoem => currentPoem;
    private bool isReturning;
    private Vector3 originalScaleSize;
    private Vector3 bigScaleSize;
    private bool isPlayingPoem;
    private bool canShowLabel = true;
    private bool hasPlayed;
    private Coroutine onSongFinishCoroutine;
    private Coroutine restoreScaleCoroutine;
    private Coroutine returnToOriginalPositionCoroutine;

    public bool CanShowLabel
    {
        get => canShowLabel;
        set => canShowLabel = value;
    }

    private void OnEnable()
    {
        onPoemStart += OnPoemStart;
    }

    private void OnDisable()
    {
        onPoemStart -= OnPoemStart;
    }

    private void OnPoemStart()
    {
        if (PoemPlayer.Instance.PublicAudioSource.clip == currentPoem.AudioClip) return;
        if (onSongFinishCoroutine != null)
        {
            StopCoroutine(onSongFinishCoroutine);
            isPlayingPoem = false;
            hasPlayed = true;
            restoreScaleCoroutine = StartCoroutine(RestoreScale());
            onSongFinishCoroutine = null;
        }
    }

    void Start()
    {
        // if (snapZone) snapZone.transform.SetParent(null);
        originalPosition = transform.position;
        originalScaleSize = transform.localScale;
        bigScaleSize = transform.localScale * scaleSizeFactor;

        currentPoem = GameManager.Instance.GetRandomPoem();
        // label.playerHead = GameManager.Instance.PlayerHead;
        SpawnRandomOrb();
        SetOrbText();
        label.SetActive(false);
    }

    private void Update()
    {
        LabelLookTowardsPlayer();
        if (!hasPlayed && !isPlayingPoem && transform.localScale == bigScaleSize)
        {
            isPlayingPoem = true;
            PoemPlayer.Instance.PlayPoem(currentPoem.AudioClip);
            onSongFinishCoroutine = StartCoroutine(StopPoemWhenFinish());
            onPoemStart?.Invoke();
        }

        if (isPlayingPoem && transform.localScale == originalScaleSize)
        {
            isPlayingPoem = false;
            PoemPlayer.Instance.StopPoem();
        }

        if (label.activeSelf) PositionLabelAtBottom();
    }

    private IEnumerator StopPoemWhenFinish()
    {
        yield return new WaitForSeconds(PoemPlayer.Instance.PublicAudioSource.clip.length - PoemPlayer.Instance.PublicAudioSource.time);
        if (PoemPlayer.Instance.PublicAudioSource.clip == currentPoem.AudioClip) PoemPlayer.Instance.StopPoem();
        isPlayingPoem = false;
        hasPlayed = true;
        restoreScaleCoroutine = StartCoroutine(RestoreScale());
    }

    private IEnumerator RestoreScale()
    {
        float lerp = 0;
        Vector3 currentScale = transform.localScale;
        while (lerp < 1)
        {
            lerp += Time.deltaTime * returnScaleSpeed;
            transform.localScale = Vector3.Lerp(currentScale, originalScaleSize, lerp);
            yield return null;
        }

        hasPlayed = false;
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

    private void LabelLookTowardsPlayer()
    {
        if (!label) return;
        label.transform.LookAt(GameManager.Instance.PlayerHead);
    }

    private void SpawnRandomOrb()
    {
        if (orbPrefabs.Length == 0) return;
        var prefab = orbPrefabs[Random.Range(0, orbPrefabs.Length)];
        Instantiate(prefab, transform.position, Quaternion.identity, transform);
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
        if (returnToOriginalPositionCoroutine != null)
        {
            StopCoroutine(returnToOriginalPositionCoroutine);
            returnToOriginalPositionCoroutine = null;
        }
        if (restoreScaleCoroutine != null)
        {
            StopCoroutine(restoreScaleCoroutine);
            restoreScaleCoroutine = null;
        }
        
        // if (isPlayingPoem)
        // {
        //     onSongFinishCoroutine = StartCoroutine(StopPoemWhenFinish());
        // }
        isReturning = false;
    }

    public void OnDeselect()
    {
        if (returnToOriginalPositionCoroutine != null)
        {
            StopCoroutine(returnToOriginalPositionCoroutine);
            returnToOriginalPositionCoroutine = null;
        }
        if (restoreScaleCoroutine != null)
        {
            StopCoroutine(restoreScaleCoroutine);
            restoreScaleCoroutine = null;
        }
        isReturning = false;
        if (!isPlayingPoem)
        {
            restoreScaleCoroutine = StartCoroutine(RestoreScale());
        }
        returnToOriginalPositionCoroutine = StartCoroutine(ReturnToOriginalPosition());
    }

    private IEnumerator ReturnToOriginalPosition()
    {
        yield return new WaitUntil(() => rb.linearVelocity.magnitude < 0.01f);
        yield return new WaitForSeconds(delayReturn);
        isReturning = true;
    }

    private void PositionLabelAtBottom()
    {
        if (label)
        {
            label.transform.position = transform.position - new Vector3(0, transform.localScale.y * 1.5f, 0);
        }
    }

    public void OnHover(PointerEvent pointerEvent)
    {
        if (!canShowLabel || labelMinDistance >
            Vector3.Distance(transform.position, GameManager.Instance.PlayerHead.position)) return;
        PositionLabelAtBottom();
        label.SetActive(true);
    }

    public void OnUnhover(PointerEvent pointerEvent)
    {
        label.SetActive(false);
    }

    public void SetLabelActive(bool active)
    {
        label.SetActive(active);
    }
}