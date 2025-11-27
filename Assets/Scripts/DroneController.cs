
using UnityEngine;
using TMPro;

public class DroneController : MonoBehaviour
{

    float F;

    float FloatingConst;

    float rotationSpeed = 3f;

    float thrust_level;

    public TMP_Text thrustText;
    public Rigidbody magnetMass;


    Rigidbody Drone;

    
    

    private bool qPressed = false;
    private bool ePressed = false;
    private bool rPressed = false;

    
    void Start()
    {
        Drone = GetComponent<Rigidbody>();
        F = 10f;
        FloatingConst = 9.81f;
        thrust_level = 1.0f;

        UpdateText();
    }

    void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            qPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ePressed = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            rPressed = true;
        }
    }

    

    


    void FixedUpdate()
    {  
        Drone.mass = 1.0f + (magnetMass.mass-1.0f);
        float angle = Vector3.Angle(transform.up, Vector3.up);

        
        if(qPressed) 
        {
            thrust_level += 1f;
            qPressed = false; 
        }
        
        if(ePressed) 
        {
            thrust_level -= 1f;
            ePressed = false; 
        }
        
        if(rPressed) 
        {
            thrust_level = 1f;
            rPressed = false;
        }
            

        float thrust_force = (thrust_level * FloatingConst)/ Mathf.Cos(angle * Mathf.Deg2Rad);

        UpdateText();
        

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
    void UpdateText()
    {
        
        thrustText.text = "Load weight: " + (thrust_level-1) + "kg";
        
    
    }
}
