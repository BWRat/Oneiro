using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;
    void Start()
    {
        offset = target.position - this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = target.position - offset; 
    }
}
