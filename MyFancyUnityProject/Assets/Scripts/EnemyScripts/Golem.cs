using System.Collections;
using System.Collections.Generic;
using EnemyScripts;
using UnityEngine;

public class Golem : GenericEnemy
{
    private GenericEnemy _genericEnemy;
    
    public enum EnemyState
    {
        PHASEONE,
        PHASETWO,
        PHASETHREE,
    }
    
    private Coroutine _hurtCoroutine;
    public EnemyState state;
    private Animator _animator;
    private Vector3 _velocity = Vector3.zero;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private bool _isDead = false;
    
    public Transform firePointRight;
    public Transform firePointLeft;
    public GameObject dagger;
    
    private Coroutine _attackCoroutine;
    private float _nextAttackTime = 0.0f;
    

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        state = EnemyState.PHASEONE;
    }
    
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        maxHp = hp;
        genericHealthBar.genericHealthBar.maxValue = maxHp;
        genericHealthBar.genericHealthBar.value = maxHp;
        state = EnemyState.PHASEONE;
        ResetPosition();
    }

    private void FixedUpdate()
    {
        MoveTowardsPlayer(player);
    }
    

    public void MoveTowardsPlayer(GameObject target)
    {
        if (!_isDead)
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
                _rigidbody2D.transform.position = newPosition;
                _animator.SetFloat("speed", distance);
            
            }
            if (direction.x != 0) _spriteRenderer.flipX = direction.x > 0;     
        }
        
    }
    
    public IEnumerator PlayDeathAnimation()
    {
        _animator.SetTrigger("die");
        _isDead = true;
        yield return new WaitForSeconds(deathDuration);
        //TODO you win!!
    }
    
    public  IEnumerator PlayHurtAnimation()
    {
        _animator.SetTrigger("hurt");
        yield return new WaitForSeconds(hurtDuration);
        _animator.ResetTrigger("hurt");
    }

    public IEnumerator AttackPlayer(GameObject target, Vector3 direction)
    {
        _nextAttackTime = Time.time + attackSpeed;
        _animator.SetTrigger("attack");
        
        //krampus specific: attack looks completed after half the animation
        yield return new WaitForSeconds(attackDuration);
        StopAttack();
    }
    
    private void Shoot()
    {
        if (state == EnemyState.PHASETWO)
        {
            ShootThree();
            return;
        }
        
        var target = player.transform.position;
        target.z = 0;
        Transform firePoint;

        if (_spriteRenderer.flipX) firePoint = firePointLeft;
        else firePoint = firePointRight;
        
        var direction = (target - firePoint.position).normalized;
        
        GameObject daggerObject = Instantiate(dagger, firePoint.position, Quaternion.identity);
        Dagger daggerScript = daggerObject.GetComponent<Dagger>();
        daggerScript.SetDirection(direction);            
        

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        daggerObject.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void ShootThree()
    {
        var target = player.transform.position;
        target.z = 0;
        Transform firePoint;

        if (_spriteRenderer.flipX) firePoint = firePointLeft;
        else firePoint = firePointRight;
        
        var direction = (target - firePoint.position).normalized;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 30;


        GameObject[] daggers = new GameObject[3];

        for (int i = 0; i < daggers.Length; i++)
        {
            daggers[i] = Instantiate(dagger, firePoint.position, Quaternion.identity);
            Dagger daggerScript = daggers[i].GetComponent<Dagger>();
            Vector3 newVector = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad)
                , 0);
            daggerScript.SetDirection(newVector);  
            daggers[i].transform.rotation = Quaternion.Euler(0, 0, angle);
            angle += 30;
        }
    }
    

    public void StopAttack()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }
        
        _animator.ResetTrigger("attack");
    }
}
