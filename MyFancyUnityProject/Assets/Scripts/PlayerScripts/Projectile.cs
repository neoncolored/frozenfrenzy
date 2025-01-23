using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 2.5f;
    private Vector3 _direction;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _direction * (speed * Time.deltaTime);
    }


    public void SetDirection(Vector3 dir)
    {
        _direction = dir.normalized;
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
