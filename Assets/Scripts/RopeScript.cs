using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RopeScript : MonoBehaviour
{
    LineRenderer lr;
    public GameObject[] myVertexes;

    public float k;
    float newton;

    int IndexofBreak =-1;
    
    public TMP_Text forceText;

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
        RopeGenerator();
    }

    void RopeGenerator()
    {
        float maxForceThisFrame = 0f;
        for(int i =1; i < myVertexes.Length; i++)
        {
            if (IndexofBreak != -1 && IndexofBreak < i) 
            {
                break; 
            }
            currentObj = myVertexes[i];
            previousObj = myVertexes[i - 1];
            Rigidbody currentRb = currentObj.GetComponent<Rigidbody>();
            Rigidbody previousRb = previousObj.GetComponent<Rigidbody>();
            deltaY = previousObj.transform.position - currentObj.transform.position;
            float currentLenght = deltaY.magnitude;
            Vector3 direction = deltaY.normalized;

            F = k * (currentLenght - restLengths[i-1])*direction;

            float currentForceMagnitude = F.magnitude;
            if (currentForceMagnitude > maxForceThisFrame)
            {
                maxForceThisFrame = currentForceMagnitude;
            }
            
            if(F.magnitude > 1000f)
            {
                IndexofBreak = i;
                //Debug.Log("Rope broke at index: " + IndexofBreak);
                continue;
            }
            if (i == 1)
            {
                currentRb.AddForce(F);
                newton = F.magnitude;
               // Debug.Log("Force in rope: " + newton + " N");
                
            }
            else
            {
                currentRb.AddForce(F);
                previousRb.AddForce(-F);
            }


        }

        int renderLength = myVertexes.Length;
        if (IndexofBreak != -1)
        {
            renderLength = IndexofBreak; 
        }
        lr.positionCount = renderLength; 
        for(int i =0; i < renderLength; i++)
        {
            lr.SetPosition(i, myVertexes[i].transform.position);
        }
        UpdateForceText(maxForceThisFrame);
    }

    void UpdateForceText(float Force)
    {
        
        forceText.text = "Max Force: 200N "+ "\n" + "Current Force: " + Force + " N";
        
    
    }
}


