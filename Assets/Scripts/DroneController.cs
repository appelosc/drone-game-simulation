using UnityEngine;

public class DroneController : MonoBehaviour
{

    float F;

    float FloatingConst;

    Rigidbody Drone;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Drone = GetComponent<Rigidbody>();
        F = 30f;
        FloatingConst = 9.81f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(Input.anyKey == false)
        {
            Drone.AddForce(Vector3.up * FloatingConst);
        }
        if (Input.GetKey(KeyCode.W) == true)
        {
            
            Drone.AddForce(Vector3.up * F);
        }
        if (Input.GetKey(KeyCode.S) == true)
        {
           
            Drone.AddForce(Vector3.down * F);
        }if (Input.GetKey(KeyCode.UpArrow) == true)
        {
            transform.Rotate( new Vector3(0.3f, 0, 0) );           
            Drone.AddForce(0,0,F);
        }
        if (Input.GetKey(KeyCode.DownArrow) == true)
        {
            Drone.AddForce(0,0,-F);

        }
        if (Input.GetKey(KeyCode.RightArrow) == true)
        {
            Drone.AddForce(F,0,0);

        }if (Input.GetKey(KeyCode.LeftArrow) == true)
        {
            Drone.AddForce(-F,0,0);

        }
        
    }
}
