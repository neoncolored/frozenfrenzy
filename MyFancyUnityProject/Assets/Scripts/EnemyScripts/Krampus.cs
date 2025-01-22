using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Krampus : MonoBehaviour
{
    public enum EnemyState
    {
        Walking,
        Attacking,
    }
    
    public float speed = 8.0f;
    public float range = 8.0f;
    
    public GameObject player;
    private Animator _animator;
    private Vector3 _velocity = Vector3.zero;
    private EnemyState _state;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    
    public float attackSpeed = 20.0f;
    public float attackDuration = 5.0f;
    public int hp = 20;
    public int damage = 20;
    
    private Coroutine _attackCoroutine;
    private float _nextAttackTime = 0.0f;
    private bool _isAttacking = false;


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
        Vector3 newPosition = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        var relativePos = transform.position - target.transform.position;
        var distance = relativePos.magnitude;
        var direction = relativePos / distance;
        if (direction.y > 0) _spriteRenderer.sortingOrder = 5;
        else
        {
            _spriteRenderer.sortingOrder = 10;}
        
        if (distance < range)
        {
            if (Time.time >= _nextAttackTime)
            {
                _attackCoroutine = StartCoroutine(AttackPlayer(target, direction));
            }
        }
        else if(_isAttacking == false)
        {
            _state = EnemyState.Walking;
            _rigidbody2D.transform.position = newPosition;
            _animator.SetFloat("speed", distance);
            
        }
        if (direction.x != 0) _spriteRenderer.flipX = direction.x > 0; 
        

    }

    public IEnumerator AttackPlayer(GameObject target, Vector3 direction)
    {
        _state = EnemyState.Attacking;
        _isAttacking = true;
        _nextAttackTime = Time.time + attackSpeed;
        _animator.SetTrigger("attack");
        
        //krampus specific: attack looks completed after half the animation
        yield return new WaitForSeconds(attackDuration/2);
        
        if ((transform.position - target.transform.position).magnitude < range) //player is hit
        {
            Player.Hp -= damage;
            Debug.Log(Player.Hp);
        }
        
        
        yield return new WaitForSeconds(attackDuration/2);
        
        StopAttack();
    }

    public void StopAttack()
    {
        if (speed > 0) _state = EnemyState.Walking;
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }
        
        _isAttacking = false;
        _animator.ResetTrigger("attack");
    }
}