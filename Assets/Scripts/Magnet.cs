using UnityEngine;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class Magnet : MonoBehaviour
{

    public List<GameObject> myBox;

    bool gaming = true;
    public static float timeElapsed = 0f;
    Rigidbody Drone;
    void Awake()
    {
    timeElapsed = 0f;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Drone = GameObject.Find("Drone").GetComponent<Rigidbody>();
        //Random spawn av boxar på havet
        for(int i = 0; i < myBox.Count; i++)
        {
            myBox[i].transform.position = new Vector3(Random.Range(-100f,180f),20f,Random.Range(-150f,80f));
        }
    }
    void Update()
    {   //timer
        if(gaming)
        {
            timeElapsed += Time.deltaTime;
            
        }
    }
    //Tar bort boxen från listan när den är levererad, refereras i DropOff scriptet.
    public void RemoveBox(GameObject box)
    {
        myBox.Remove(box);

        
        if (myBox.Count == 0)
        {
            Debug.Log("Du vann spelet är slut");
            StopTimer();
            
            SceneManager.LoadScene("ResultScene");
        }
    }

    // Update is called once per frame
    

    //Räknar ut avstånd mellan magneten och boxarna och applicerar en dragningskraft
    //när boxen är tillräckligt nära så klamrar den fast boxen till drönaren
    void FixedUpdate()
    {   
        for(int i=0; i < myBox.Count; i++)
        {
            Vector3 direction = transform.position - myBox[i].transform.position;
            float distance = direction.magnitude;
            Rigidbody boxRb = myBox[i].GetComponent<Rigidbody>();
            
            if(Input.GetKey(KeyCode.Space)==true)
            {
                realeaseBox(myBox[i]);
            }
            
            if(distance < 5f && Input.GetKey(KeyCode.Space)==false)
                {
                   
                    AttachBox(boxRb.gameObject);
                }
            else if(distance < 10f && Input.GetKey(KeyCode.Space)==false)
            {
                Vector3 force = direction.normalized * (10f-distance)*3f * boxRb.mass;
                boxRb.AddForce(force);
            }

        }
    }
    

    // Klamrar fast boxen till drönaren och lägger till boxens massa till magneten för att
    //få kraften mellan sfärerna att fungera korrekt och kunna räkna ut n
    //när repet skall brista
    void AttachBox(GameObject box)
    {
        if(box.GetComponent<FixedJoint>() != null)
        {
            return;
        }
        FixedJoint joint = box.AddComponent<FixedJoint>();
        joint.connectedBody = GetComponent<Rigidbody>();
        Drone.mass += box.GetComponent<Rigidbody>().mass;


        box.transform.position = transform.position;
        box.transform.rotation = transform.rotation;
    }
    void realeaseBox(GameObject box)
    {
        FixedJoint joint = box.GetComponent<FixedJoint>();
        if(joint != null)
        {
            Destroy(joint);
            Drone.mass -= box.GetComponent<Rigidbody>().mass;
        }
    }
    void StopTimer()
    {
        gaming = false;
    }   
}
