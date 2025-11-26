using UnityEngine;

public class Magnet : MonoBehaviour
{
    public GameObject[] myBox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i < myBox.Length; i++)
        {
            Vector3 direction = transform.position - myBox[i].transform.position;
            float distance = direction.magnitude;
            if(distance < 10f)
            {
                Vector3 force = direction.normalized * (10f - distance) * 5f;
                Rigidbody boxRb = myBox[i].GetComponent<Rigidbody>();
                boxRb.AddForce(force);
            }
        }
    }
}
