using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using NUnit.Framework;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] Poem[] poems;
    [SerializeField] int poemsPerGame = 3;
    [SerializeField] FindSpawnPositions findSpawnPositions;

    private List<Poem> usedPoems = new List<Poem>();
    private static GameManager instance;
    public static GameManager Instance => instance;

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
}