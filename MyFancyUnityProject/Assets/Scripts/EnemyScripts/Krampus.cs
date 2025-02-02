using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Krampus : GenericEnemy
{
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
    private bool isDead = false;
    
    
    private Coroutine _attackCoroutine;
    private float _nextAttackTime = 0.0f;
    

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _state = EnemyState.Walking;
    }
    
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();
        maxHp = hp;
        genericHealthBar.genericHealthBar.maxValue = maxHp;
        genericHealthBar.genericHealthBar.value = maxHp;
        _state = EnemyState.Walking;
        ResetPosition();
    }

    private void FixedUpdate()
    {
        MoveTowardsPlayer(player);
    }
    

    public void MoveTowardsPlayer(GameObject target)
    {
        if (_state != EnemyState.Dying && _state != EnemyState.Hurting && !isDead)
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
            else 
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
        isDead = true;
        yield return new WaitForSeconds(deathDuration);
        
        Destroy(gameObject);
    }
    
    public  IEnumerator PlayHurtAnimation()
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
        _nextAttackTime = Time.time + attackSpeed;
        _animator.SetTrigger("attack");
        
        //krampus specific: attack looks completed after half the animation
        yield return new WaitForSeconds(attackDuration/2);
        
        if ((transform.position - target.transform.position).magnitude < range) //player is hit
        {
            playerScript.DamagePlayer(damage, transform);
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
        
        _animator.ResetTrigger("attack");
    }
}
