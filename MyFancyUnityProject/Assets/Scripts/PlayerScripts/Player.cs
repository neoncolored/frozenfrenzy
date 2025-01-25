using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private enum PlayerState
    {
        Idle,
        Walking,
        Spinning,
        Rolling,
        Hit
    }
    
    //SFX
    [SerializeField] private AudioClip[] walkClips;
    [SerializeField] private float timeForOneStep = 0.3f;
    private float _nextStepTime = 0.0f;
    
    
    //------
    
    private Player _player;
    private PlayerState _state;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    private Vector3 _velocity = Vector3.zero;
    public float speed = 50.0f;
    public static int maxHp = 100;
    public int hp = maxHp;
    
    // Rolling variables
    public float rollCoolDown = 20.0f;
    public float rollDuration = 5.0f;
    private float _timeSinceRoll = 0.0f;
    
    private Coroutine _rollCoroutine;
    private float _nextRollTime = 0.0f;
    private bool _isRolling = false;

    public GameObject fishProjectile;
    public Transform firePoint;
    public PlayerHealthBar hpBar;
    public PlayerRollStamina playerRollStamina;
    public HideStaminaBar h;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        _state = PlayerState.Idle;
        playerRollStamina.setValue(1);

    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    // Update is called once per frame
    private void Update()
    {
        PlayerInput();
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        
        
    }

    private void PlayerInput()
    {
        if (speed == 0)
        {
            _state = PlayerState.Idle;
        }
        _velocity.y = Input.GetAxis("Vertical") * speed;
        _velocity.x = Input.GetAxis("Horizontal") * speed;
        _velocity = Vector2.ClampMagnitude(_velocity, speed);  
        // maxLength muss immer = speed sein, damit man diagonal nicht schenller ist
        
        if (_velocity.magnitude > 0.01 && Time.time >= _nextStepTime && _state == PlayerState.Walking)
        {
            SoundFXManager.instance.PlayRandomSoundFXClip(walkClips, transform, 0.3f);
            _nextStepTime = Time.time + timeForOneStep;
        }
        
        _timeSinceRoll += Time.deltaTime;
        playerRollStamina.setValue(Mathf.Clamp(_timeSinceRoll/rollCoolDown, 0, 100));
        
        
        if (Input.GetKeyDown(KeyCode.E) && _state != PlayerState.Rolling)
        {
            _animator.SetTrigger("spin");
            _state = PlayerState.Spinning;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !_isRolling && _state != PlayerState.Spinning)
        {
            if (Time.time >= _nextRollTime)
            {
                _rollCoroutine = StartCoroutine(Roll());
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && _isRolling) StopRoll();
        
    }

    private void PlayerMove()
    {
        _state = PlayerState.Walking;
        _animator.SetFloat("speed", _velocity.magnitude);
        if (_velocity.x != 0) _spriteRenderer.flipX = _velocity.x < 0;
        _rigidbody2D.velocity = _velocity * Time.fixedDeltaTime;
        
        
        
    }

    private IEnumerator Roll()
    {
        h.SetActive();
        _timeSinceRoll = 0.0f;
        _state = PlayerState.Rolling;
        _isRolling = true;
        _nextRollTime = Time.time + rollCoolDown;
        _animator.SetTrigger("roll");
        
        yield return new WaitForSeconds(rollDuration);
        
        StopRoll();
    }

    public void DamagePlayer(int damage)
    {
        if (_state != PlayerState.Rolling)
        {
            hp -= damage;
            _animator.SetTrigger("getHit");
            hpBar.healthBar.value = hp; 
        }
        
    }


    private void StopRoll()
    {
        if (speed > 0) _state = PlayerState.Walking;
        if (_rollCoroutine != null)
        {
            StopCoroutine(_rollCoroutine);
            _rollCoroutine = null;
        }
        
        _isRolling = false;
        _animator.ResetTrigger("roll");
        _animator.Play("walk");
        StartCoroutine(h.SetInactive());
    }

    private void Shoot()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        var direction = (mousePos - firePoint.position).normalized;
        
        GameObject projectile = Instantiate(fishProjectile, firePoint.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.SetDirection(direction);            
        

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
