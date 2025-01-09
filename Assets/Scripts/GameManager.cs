using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using NUnit.Framework;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] Poem[] poems;
    [SerializeField] int poemsPerGame = 3;
    [SerializeField] FindSpawnPositions findSpawnPositions;
    [SerializeField] OVRPassthroughLayer passthroughLayer;
    [SerializeField] private Transform playerHead;

    private List<Poem> usedPoems = new List<Poem>();
    private static GameManager instance;
    private float originalBrightness;
    public static GameManager Instance => instance;
    public Transform PlayerHead => playerHead;

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
        findSpawnPositions.SpawnAmount = poemsPerGame;
    }
    private void Start()
    {
        originalBrightness = passthroughLayer.colorMapEditorBrightness;
    }

    public Poem GetRandomPoem()
    {
        if (poems.Length == 0) return null;
        if (usedPoems.Count == poems.Length)
        {
            return null;
        }

        Poem poem;
        do
        {
            poem = poems[Random.Range(0, poems.Length)];
        } while (usedPoems.Contains(poem));

        usedPoems.Add(poem);
        return poem;
    }
    private IEnumerator PassthroughFadeToBlack(float fadeDuration)
    {
        float lerp = 0;
        float currentBrightness = passthroughLayer.colorMapEditorBrightness;
        while (lerp < 1)
        {
            lerp += Time.deltaTime / fadeDuration;
            passthroughLayer.colorMapEditorBrightness = Mathf.Lerp(currentBrightness, -0.9f, lerp);
            yield return null;
        }
        passthroughLayer.colorMapEditorBrightness = -1;
        yield return null;
    }
    private IEnumerator PassthroughFadeToOriginal(float fadeDuration)
    {
        float lerp = 0;
        float currentBrightness = passthroughLayer.colorMapEditorBrightness;
        while (lerp < 1)
        {
            lerp += Time.deltaTime / fadeDuration;
            passthroughLayer.colorMapEditorBrightness = Mathf.Lerp(currentBrightness, originalBrightness, lerp);
            yield return null;
        }
        passthroughLayer.colorMapEditorBrightness = originalBrightness;
        yield return null;
    }
    
    public void StartPassthroughFadeToBlack(float fadeDuration)
    {
        StopAllCoroutines();
        StartCoroutine(PassthroughFadeToBlack(fadeDuration));
    }
    public void StartPassthroughFadeToOriginal(float fadeDuration)
    {
        StopAllCoroutines();
        StartCoroutine(PassthroughFadeToOriginal(fadeDuration));
    }
}