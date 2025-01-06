using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Serialization;

public class PoeticSphere : MonoBehaviour
{
    [SerializeField] SnapInteractable snapZone;

    void Start()
    {
        if (snapZone) snapZone.transform.SetParent(null);
    }
}