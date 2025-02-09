using System.Collections;
using System.Collections.Generic;
using EnemyScripts;
using Managers;
using PlayerScripts;
using ScreenScripts;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Golem : GenericEnemy
{
    private GenericEnemy _genericEnemy;
    
    public enum EnemyState
    {
        PHASEONE,
        PHASETWO,
        PHASETHREE,
        PHASEFOUR,
    }
    
    private Coroutine _hurtCoroutine;
    public EnemyState state;
    private Animator _animator;
    private Vector3 _velocity = Vector3.zero;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private bool _isDead = false;
    private bool _isAttacking;
    public bool isInvulnerable = false;
    private bool _isWalkingToCampfire = false;

    private GameObject campfireobj;
    private Transform campfire;
    public Transform firePointRight;
    public Transform firePointLeft;
    public Transform firePoint;
    public GameObject dagger;
    
    private Coroutine _attackCoroutine;
    private float _nextAttackTime = 0.0f;
    public float nextSpecialAttackTime = 0.0f;
    public float specialAttackDuration;
    public float specialAttackSpeed = 2.0f;
    private float _nextSubSpecialAttackTime = 0.0f;
    private bool _preparingForAttack;
    private Coroutine _specialAttackCoroutine;
    public WinScreenManager winScreenManager;

    public AudioClip BossSpawn;
    

    private new void Awake()
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
        campfireobj = GameObject.FindWithTag("Campfire");
        campfire = campfireobj.GetComponent<Campfire>().posForBoss;
        state = EnemyState.PHASEONE;
        ResetPosition();
        SoundFXManager.instance.PlaySoundFXClip(BossSpawn, transform, 0.1f, false, false);
        BackgroundMusic.Instance.StopSong(0);
        BackgroundMusic.Instance.PlaySong(1);
    }

    private void FixedUpdate()
    {
        MoveTowardsPlayer(player);
    }
    

    public void MoveTowardsPlayer(GameObject target)
    {
        if (isInvulnerable && Time.time >= _nextSubSpecialAttackTime && _isAttacking)
        {
            StartCoroutine(DoSpecialAttack(UnityEngine.Random.Range(10,20)));
            return;
        }
        if(isInvulnerable) return;
        
        if (_isWalkingToCampfire)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, campfire.position, speed * 2 * Time.deltaTime);
            var relativePos = transform.position - campfire.position;
            var distance = relativePos.magnitude;
            var direction = relativePos / distance;
            if (direction.y > 0) _spriteRenderer.sortingOrder = 5;
            else _spriteRenderer.sortingOrder = 10;
            _rigidbody2D.transform.position = newPosition;
            if (distance < 0.1 && !_preparingForAttack)
            {
                _preparingForAttack = true;
                _specialAttackCoroutine = StartCoroutine(SpecialAttack());
            }
            _animator.SetFloat("speed", distance);
            if (direction.x != 0) _spriteRenderer.flipX = direction.x > 0;   
        }

        if (!_isDead)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            var relativePos = transform.position - target.transform.position;
            var distance = relativePos.magnitude;
            var direction = relativePos / distance;
            if (direction.y > 0) _spriteRenderer.sortingOrder = 5;
            else
            {
                _spriteRenderer.sortingOrder = 10;
                
            }
            if (Time.time >= _nextAttackTime)
            {
                if ((state == EnemyState.PHASETHREE || state == EnemyState.PHASEFOUR) && Time.time >= nextSpecialAttackTime)
                {
                    StartCoroutine(WalkToCampfireAndStartSpecialAttack());
                }
                if(distance < range) _attackCoroutine = StartCoroutine(AttackPlayer(target, direction));
            }

            if (distance >= range)
            {
                _rigidbody2D.transform.position = newPosition;
                _animator.SetFloat("speed", distance);
            }
            
            if (direction.x != 0) _spriteRenderer.flipX = direction.x > 0;     
        }
        
    }

    public IEnumerator WalkToCampfireAndStartSpecialAttack() // why an iEnumerator??
    {
        _isWalkingToCampfire = true;
        yield return new WaitForSeconds(0);
    }
    
    public IEnumerator PlayDeathAnimation()
    {
        _animator.SetTrigger("die");
        _isDead = true;
        yield return new WaitForSeconds(deathDuration);
        BackgroundMusic.Instance.StopSong(1);
        BackgroundMusic.Instance.PlaySong(0);
        winScreenManager.ShowWinScreen();

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
        yield return new WaitForSeconds(attackDuration);
        StopAttack();
    }
    
    public IEnumerator SpecialAttack()
    {
        Color original = genericHealthBar.image.color;
        genericHealthBar.image.color = Color.black;
        nextSpecialAttackTime = Time.time + specialAttackDuration;
        _animator.SetTrigger("special");
        yield return new WaitForSeconds(1);
        isInvulnerable = true;
        yield return new WaitForSeconds(1);
        _isAttacking = true;
        _isWalkingToCampfire = false;
        _preparingForAttack = false;
        float specialAttackCooldown = specialAttackDuration / 2 - 2; // i think cooldown and duration is switched
        yield return new WaitForSeconds(specialAttackCooldown-1); //not very clean code all the durations get mixed up
        StopSpecialAttack();
        genericHealthBar.image.color = original;
    }

    private int count = 0;
    
    private IEnumerator DoSpecialAttack(int daggerAmount)
    {
        var direction = Vector3.right.normalized;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        switch (count)
        {
            case 0:
            {
                angle += 10;
                count++;
                break;
            }
            case 1:
            {
                angle += 20;
                count++;
                break;
            }
            case 2:
            {
                count = 0;
                break;
            }
            default:
            {
                count = 0;
                break;
            }
        }

        GameObject[] daggers = new GameObject[daggerAmount];

        for (int i = 0; i < daggers.Length; i++)
        {
            daggers[i] = Instantiate(dagger, firePoint.position, Quaternion.identity);
            Dagger daggerScript = daggers[i].GetComponent<Dagger>();
            Vector3 newVector = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad)
                , 0);
            daggerScript.SetDirection(newVector);  
            daggers[i].transform.rotation = Quaternion.Euler(0, 0, angle);
            angle += 360.0f/daggers.Length;
        }

        _nextSubSpecialAttackTime = Time.time + specialAttackSpeed;
        
        yield return new WaitForSeconds(0);
    }
    
    public void StopSpecialAttack()
    {

        _isAttacking = false;
        isInvulnerable = false;
        _animator.ResetTrigger("special");
    }
    
    
    
    private void Shoot()
    {
        if (state == EnemyState.PHASETWO || state == EnemyState.PHASETHREE || state == EnemyState.PHASEFOUR)
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
