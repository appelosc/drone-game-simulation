using System;
using UnityEngine;

public class RopeScript : MonoBehaviour
{
    LineRenderer lr;
    public GameObject[] myVertexes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.startWidth = 0.4f;
        lr.startWidth = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        
        for(int i =0; i < myVertexes.Length; i++)
        {
            lr.SetPosition(i, myVertexes[i].transform.position);
        }
        
    }
}
