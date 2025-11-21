
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
        F = 10f;
        FloatingConst = 9.81f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {  
        float angle = Vector3.Angle(transform.up, Vector3.up);
        float thrust_force = (Drone.mass * FloatingConst) / Mathf.Cos(angle * Mathf.Deg2Rad);
        

        if (Input.GetKey(KeyCode.W))
        {
            thrust_force += F;  
        }

        if (Input.GetKey(KeyCode.S))
        {
            thrust_force -= F;    
        }

        float targetX = 0f;
        if (Input.GetKey(KeyCode.UpArrow))     targetX = 40f;
        if (Input.GetKey(KeyCode.DownArrow))   targetX = -40f;

        float targetZ = 0f;
        if (Input.GetKey(KeyCode.LeftArrow))   targetZ = 40f;
        if (Input.GetKey(KeyCode.RightArrow))  targetZ = -40f;

        
        float yaw = Drone.rotation.eulerAngles.y;

        
        Quaternion targetRot = Quaternion.Euler(targetX, yaw, targetZ);
        Quaternion newRot = Quaternion.Lerp(Drone.rotation, targetRot, rotationSpeed * Time.fixedDeltaTime);

        Drone.MoveRotation(newRot);
        Drone.AddForce(transform.up * thrust_force);
        
    }
}
