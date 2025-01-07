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
        poemPlayer.gameObject.SetActive(false);
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PoeticSphere"))
        {
            if (sphereCount == 0) poemPlayer.gameObject.SetActive(true);
            sphereCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PoeticSphere"))
        {
            sphereCount--;
            if (sphereCount == 0 && !poemPlayer.HasPoem) poemPlayer.gameObject.SetActive(false);
        }
    }
}