using System;
using Unity.VisualScripting;
using UnityEngine;

public class RopeScript : MonoBehaviour
{
    LineRenderer lr;
    public GameObject[] myVertexes;

    public float k;
    float newton;

    Boolean breakRope = false;

    GameObject previousObj;

    GameObject currentObj;

    Vector3 deltaY;

    float f;

    float[] restLengths;

    Vector3 F;

    public float initialRestLength = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.startWidth = 0.4f;
        lr.startWidth = 0.4f;

        restLengths = new float[myVertexes.Length - 1];

    for (int i = 0; i < restLengths.Length; i++)
    {
        restLengths[i] = initialRestLength;
    }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(breakRope) return;

        RopeGenerator();

        if(newton > 200f)
        {
        breakRope = true;
        Debug.Log("Rope broke!");
        lr.GameObject().SetActive(false);
        }
        
    }

    void RopeGenerator()
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
                
                newton = F.magnitude;
                //Debug.Log("Force in rope: " + newton + " N");
                
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


