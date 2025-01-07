using System;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PoeticSphere : MonoBehaviour
{
    [SerializeField] SnapInteractable snapZone;
    [SerializeField] GameObject[] orbPrefabs;
    [SerializeField] private PointableUnityEventWrapper unityEvent;

    private Poem currentPoem;
    private GameObject currentOrb;
    public Poem CurrentPoem => currentPoem;

    void Start()
    {
        if (snapZone) snapZone.transform.SetParent(null);
        SpawnRandomOrb();
        currentPoem = GameManager.Instance.GetRandomPoem();
    }

    private void SpawnRandomOrb()
    {
        if (orbPrefabs.Length == 0) return;
        var prefab = orbPrefabs[Random.Range(0, orbPrefabs.Length)];
        currentOrb = Instantiate(prefab, transform.position, Quaternion.identity, transform);
    }
}