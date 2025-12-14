using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //drönaren
    public Transform target;          
    public Vector3 offset = new Vector3(0, 30, -40);
    public float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        //ändrar kamerans position baserat hur man styr drönaren för att enklare kunna se drönaren
        if (target == null) return;
        offset = new Vector3(0, 30, -40);
        if(Input.GetKey(KeyCode.RightArrow))
        {
            offset = new Vector3(-40, 10, 0);
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            offset = new Vector3(40, 10, 0);
        }
        
        Vector3 targetPosition = target.position + offset;

        //smoothtime för att kameran ska röra sig mjukt mot drönaren
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        //så att kameran alltid kollar mot drönaren
        transform.LookAt(target);
    }
}
