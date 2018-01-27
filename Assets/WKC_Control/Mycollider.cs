using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mycollider : MonoBehaviour
{
    GameObject[] coliders;
    void Start()
    {
        coliders = GameObject.FindGameObjectsWithTag("Obsticle");
    }

    void Update()
    {
        foreach (GameObject go in coliders)
        {

        }
    }
}