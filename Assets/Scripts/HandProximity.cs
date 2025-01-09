using System;
using System.Collections;
using Oculus.Interaction;
using UnityEngine;

public class HandProximity : MonoBehaviour
{
    [SerializeField] PoemPlayer poemPlayer;
    private static int sphereCount = 0;

    private void Start()
    {
        // poemPlayer.gameObject.SetActive(false);
    }

    private PoeticSphere sphere;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PoeticSphere"))
        {
            sphereCount++;
            sphere = other.GetComponent<PoeticSphere>();
            sphere.CanShowLabel = false;
            sphere.SetLabelActive(false); 
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PoeticSphere"))
        {
            sphereCount--;
            if (sphereCount != 0) return;
            sphere.CanShowLabel = true;
        }
    }
}