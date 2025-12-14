using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RopeScript : MonoBehaviour
{
    LineRenderer lr;
    public GameObject[] myVertexes;

    public float k;
    bool restartGame = false;

    int IndexofBreak =-1;
    
    public TMP_Text forceText;

    GameObject previousObj;

    GameObject currentObj;

    Vector3 deltaY;
    float[] restLengths;

    Vector3 F;

    //ser till att vilolängden är densamma för varje segment i repet
    public float initialRestLength = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   //definierar line renderer och dess bredd
        lr = GetComponent<LineRenderer>();
        lr.startWidth = 0.4f;
        lr.startWidth = 0.4f;
        //sätter vilolängden för varje segment i repet
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
    {   //variabel för att hålla koll på den maximala kraften i repet varje frame
        float maxForceThisFrame = 0f;
        for(int i =1; i < myVertexes.Length; i++)
        {   //avslutar loopen om repet har gått av
            if (IndexofBreak != -1 && IndexofBreak < i) 
            {
                break; 
            }
            currentObj = myVertexes[i];
            previousObj = myVertexes[i - 1];

            Rigidbody currentRb = currentObj.GetComponent<Rigidbody>();
            Rigidbody previousRb = previousObj.GetComponent<Rigidbody>();
            //räknar ut längden på segmentet för varje del av repet
            deltaY = previousObj.transform.position - currentObj.transform.position;
            float currentLenght = deltaY.magnitude;
            Vector3 direction = deltaY.normalized;
            //Hookes lag för att räkna ut kraften i repet
            F = k * (currentLenght - restLengths[i-1])*direction;

            //loop för att hålla koll på den maximala kraften i repet varje frame
            float currentForceMagnitude = F.magnitude;
            if (currentForceMagnitude > maxForceThisFrame)
            {
                maxForceThisFrame = currentForceMagnitude;
            }
            //om kraften i repet överstiger 200N så går repet av
            if(F.magnitude > 200f && i!=6) // undviker att repet går av vid sista sfären AKA magneten
            {
                IndexofBreak = i;
                Debug.Log("Rope broke at index: " + IndexofBreak);
                continue;
            }
            //första sfären är statisk, applicerar kraften endast på den andra sfären mot första
            if (i == 1)
            {
                currentRb.AddForce(F); 
            }
            // på resten av sfärerna appliceras kraften åt båda hållen
            else
            {
                currentRb.AddForce(F);
                previousRb.AddForce(-F);
            }


        }

        // ifall repet får av så renderas endast de delar som är kvar av repet

        int renderLength = myVertexes.Length;
        if (IndexofBreak != -1)
        {
            renderLength = IndexofBreak; 
            restartGame = true;
        }
        lr.positionCount = renderLength; 
        for(int i =0; i < renderLength; i++)
        {
            lr.SetPosition(i, myVertexes[i].transform.position);
        }
        UpdateForceText(maxForceThisFrame);
        // om repet har gått av så laddas restartscenen
        if (restartGame)
        {
            SceneManager.LoadScene("RestartScene");
        }
    }

    void UpdateForceText(float Force)
    {
        
        forceText.text = "Max Force: 200N "+ "\n" + "Current Force: " + Force + " N";
        
    
    }
}


