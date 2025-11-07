using System;
using UnityEngine;

public class WaveSimulation : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    Vector3[] originalVertices;

    public float Amplitude;
    public float Frequency;
    public float Wavelength;


    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        
    }

    void Update()
    {
        for (var i = 0; i < vertices.Length; i++)
        {
            Vector3 v = vertices[i];
            float x = v.x + v.z; 

            
            float y = Amplitude * Mathf.Sin(2 * Mathf.PI / Wavelength * x - 2 * Mathf.PI * Frequency * Time.time);

            v.y = y;
            vertices[i] = v;
        }

        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        
    }
}
