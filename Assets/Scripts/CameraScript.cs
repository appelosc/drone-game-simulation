using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;          // Drönaren
    public Vector3 offset = new Vector3(0, 5, -10);
    public float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        // Beräkna önskad position
        Vector3 targetPosition = target.position + offset;

        // Smidig övergång mot positionen
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // Titta på drönaren
        transform.LookAt(target);
    }
}
