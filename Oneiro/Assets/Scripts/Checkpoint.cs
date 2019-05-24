using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Checkpoint lastCheckPoint;
    void Start()
    {
        //Checkpoint = FindObjectOfType<Respawn>;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            SaveCheckPoint(other.transform);
        }
    }

    private void SaveCheckPoint(Transform player)
    {
        lastCheckPoint = this;
    }

    private void LoadCheckPoint(Transform player)
    {
        player.position = this.transform.position;
    }
}
