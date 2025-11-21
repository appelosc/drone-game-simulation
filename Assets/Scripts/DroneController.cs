
using UnityEngine;

public class DroneController : MonoBehaviour
{

    float F;

    float FloatingConst;

    float rotationSpeed = 3f;


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
        float angle = Vector3.Angle(transform.up, Vector3.up);
        float thrust_force = (Drone.mass * FloatingConst) / Mathf.Cos(angle * Mathf.Deg2Rad);
        Drone.AddForce(transform.up * thrust_force);

        if(Input.anyKey == false)
        {
            Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

        }

        if (Input.GetKey(KeyCode.W) == true)
        {
            
            Drone.AddForce(Vector3.up * F);
        }

        if (Input.GetKey(KeyCode.S) == true)
        {
           
            Drone.AddForce(Vector3.down * F);
    
        }

        if (Input.GetKey(KeyCode.UpArrow) == true)
        {
            Quaternion targetRotation = Quaternion.Euler(30f, Drone.rotation.eulerAngles.y, Drone.rotation.eulerAngles.z);
            Quaternion newRotation = Quaternion.Lerp(Drone.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            Drone.MoveRotation(newRotation);
            
            Drone.AddForce(Vector3.forward * F);
        }

        if (Input.GetKey(KeyCode.DownArrow) == true)
        {
            Quaternion targetRotation = Quaternion.Euler(-30f, Drone.rotation.eulerAngles.y, Drone.rotation.eulerAngles.z);
            Quaternion newRotation = Quaternion.Lerp(Drone.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            Drone.MoveRotation(newRotation);
            
            Drone.AddForce(Vector3.back * F);
            
        }

        if (Input.GetKey(KeyCode.RightArrow) == true)
        {
            Quaternion targetRotation = Quaternion.Euler(Drone.rotation.eulerAngles.x, Drone.rotation.eulerAngles.y, -30f);
            Quaternion newRotation = Quaternion.Lerp(Drone.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            Drone.MoveRotation(newRotation);
            
            Drone.AddForce(Vector3.right * F);

        }
        
        if (Input.GetKey(KeyCode.LeftArrow) == true)
        {
            Quaternion targetRotation = Quaternion.Euler(Drone.rotation.eulerAngles.x, Drone.rotation.eulerAngles.y, 30f);
            Quaternion newRotation = Quaternion.Lerp(Drone.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            Drone.MoveRotation(newRotation);
            Drone.AddForce(Vector3.left * F);

        }
        
    }
}
