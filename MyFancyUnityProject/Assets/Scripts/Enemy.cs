using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        Walking,
    }
    
    public float speed = 8.0f;
    public float range = 8.0f;
    
    public GameObject player;
    private Animator _animator;
    private Vector3 _velocity = Vector3.zero;
    private EnemyState _state;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetPosition();
    }

    private void FixedUpdate()
    {
        MoveTowardsPlayer(player);
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    public void ResetPosition()
    {
        transform.position = new Vector2(Random.Range(-1.0f, 6.0f), Random.Range(-2.0f, 1.0f));
    }

    public void MoveTowardsPlayer(GameObject target)
    {   
        _state = EnemyState.Walking;
        
        Vector3 newPosition = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        var relativePos = transform.position - target.transform.position;
        var distance = relativePos.magnitude;
        var direction = relativePos / distance;
        
        _rigidbody2D.transform.position = newPosition;
        _animator.SetFloat("speed", newPosition.magnitude);
        if (direction.x != 0) _spriteRenderer.flipX = direction.x > 0;
        if (distance < range) AttackPlayer(target);

    }

    public void AttackPlayer(GameObject target)
    {
        
    }
}