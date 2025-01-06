using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Serialization;

public class PoeticSphere : MonoBehaviour
{
    [SerializeField] SnapInteractable snapZone;
    [SerializeField] GameObject[] orbPrefabs;

    private GameObject currentOrb;

    void Start()
    {
        if (snapZone) snapZone.transform.SetParent(null);
        SpawnRandomOrb();
    }

    private void SpawnRandomOrb()
    {
        if (orbPrefabs.Length == 0) return;
        var prefab = orbPrefabs[Random.Range(0, orbPrefabs.Length)];
        currentOrb = Instantiate(prefab, transform.position, Quaternion.identity, transform);
    }
}