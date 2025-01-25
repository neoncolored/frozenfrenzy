using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EvilSnowman : GenericEnemy
{
    public GameObject player;
    private GenericEnemy _genericEnemy;
    
    private enum EnemyState
    {
        Walking,
        Attacking,
        Dying,
        Hurting,
    }

    private Player playerScript;
    private Coroutine _hurtCoroutine;
    private EnemyState _state;
    private Animator _animator;
    private Vector3 _velocity = Vector3.zero;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    
    
    private Coroutine _attackCoroutine;
    private float _nextAttackTime = 0.0f;
    private bool _isAttacking = false;
    
    public Transform firePoint;
    public GameObject snowball;
    

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        playerScript = player.GetComponent<Player>();
        _state = EnemyState.Walking;
    }

    private void FixedUpdate()
    {
        MoveTowardsPlayer(player);
    }
    

    public void MoveTowardsPlayer(GameObject target)
    {
        if (_state != EnemyState.Dying && _state != EnemyState.Hurting)
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
        
    }
    
    public IEnumerator PlayDeathAnimation()
    {
        _animator.SetTrigger("die");
        _state = EnemyState.Dying;
        yield return new WaitForSeconds(deathDuration);
        
        Destroy(gameObject);
    }
    
    public IEnumerator PlayHurtAnimation()
    {
        _animator.SetTrigger("hurt");
        _state = EnemyState.Hurting;
        yield return new WaitForSeconds(hurtDuration);
        _state = EnemyState.Walking;
        _animator.ResetTrigger("hurt");
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
            Shoot();
        }
        
        
        yield return new WaitForSeconds(attackDuration/2);
        
        StopAttack();
    }
    
    private void Shoot()
    {
        var target = player.transform.position;
        target.z = 0;
        var direction = (target - firePoint.position).normalized;
        
        GameObject ball = Instantiate(snowball, firePoint.position, Quaternion.identity);
        Snowball snowballScript = ball.GetComponent<Snowball>();
        snowballScript.SetDirection(direction);            
        

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        ball.transform.rotation = Quaternion.Euler(0, 0, angle);
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
