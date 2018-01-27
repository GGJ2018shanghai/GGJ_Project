using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Vector3 offset = new Vector3();
    private Transform target;
    private void Start()
    {
        target = GameObject.Find("Player").transform;
        offset = this.transform.position - target.position;
    }
    void Update()
    {
        this.transform.position = target.position + offset;
    }
}
