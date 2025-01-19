using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    
    private Vector3 _velocity = Vector3.zero;
    public float speed = 10.0f;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
        _velocity.y = Input.GetAxis("Vertical") * speed;
        _velocity.x = Input.GetAxis("Horizontal") * speed;
        _velocity = Vector2.ClampMagnitude(_velocity, 50.0f); 
        // maxLength muss immer = speed sein, damit man diagonal nicht schenller ist
    }

    private void PlayerMove()
    {
        _animator.SetFloat("speed", Math.Abs(_velocity.x));
        if (_velocity.x != 0) _spriteRenderer.flipX = _velocity.x < 0;
        _rigidbody2D.velocity = _velocity * Time.fixedDeltaTime;
    }
}
