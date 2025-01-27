using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    public float amplitude = 10f;  
    public float speed = 1f;      

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;  
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * speed) * amplitude;
        float x = Mathf.Cos(Time.time * speed) * amplitude * 0.5f; 
        transform.position = startPosition + new Vector3(x, y, 0);
    }
}
