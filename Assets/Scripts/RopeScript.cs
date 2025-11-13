using System;
using UnityEngine;

public class RopeScript : MonoBehaviour
{
    LineRenderer lr;
    public GameObject[] myVertexes;

    public float k;

    GameObject previousObj;

    GameObject currentObj;

    Vector3 deltaY;

    float f;

    float[] restLengths;

    Vector3 F;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.startWidth = 0.4f;
        lr.startWidth = 0.4f;

        restLengths = new float[myVertexes.Length - 1];

    for (int i = 1; i < myVertexes.Length; i++)
    {
        Vector3 delta = myVertexes[i - 1].transform.position - myVertexes[i].transform.position;
        restLengths[i - 1] = delta.magnitude;
    }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for(int i =1; i < myVertexes.Length; i++)
        {
            currentObj = myVertexes[i];
            previousObj = myVertexes[i - 1];
            Rigidbody currentRb = currentObj.GetComponent<Rigidbody>();
            Rigidbody previousRb = previousObj.GetComponent<Rigidbody>();
            deltaY = previousObj.transform.position - currentObj.transform.position;
            float currentLenght = deltaY.magnitude;
            Vector3 direction = deltaY.normalized;

            F = k * (currentLenght - restLengths[i-1])*direction;

            if (i == 1)
            {
                currentRb.AddForce(F);
            }
            else
            {
                currentRb.AddForce(F);
                previousRb.AddForce(-F);
            }


        }

        for(int i =0; i < myVertexes.Length; i++)
        {
            lr.SetPosition(i, myVertexes[i].transform.position);
        }
        
    }
}
