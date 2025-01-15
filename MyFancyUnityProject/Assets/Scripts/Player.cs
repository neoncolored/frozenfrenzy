using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator playerAnimator;
    
    private Vector3 m_velocity = Vector3.zero;
    public float speed = 10.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    private void PlayerInput()
    {
        m_velocity.y = Input.GetAxis("Vertical") * speed;
        m_velocity.x = Input.GetAxis("Horizontal") * speed;
    }

    private void PlayerMove()
    {
        rb.velocity = m_velocity * Time.fixedDeltaTime;
    }
}
