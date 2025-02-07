using System;
using System.Collections;
using Managers;
using ScreenScripts;
using UnityEngine;
using Waves;
using PlayerScripts.UI;


namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public enum PlayerState
        {
            Idle,
            Walking,
            Spinning,
            Rolling,
            Hit,
            Dead,
            Reloading
        }
    
        //SFX
        [SerializeField] private AudioClip[] walkClips;
        [SerializeField] private float timeForOneStep = 0.3f;

        private float _nextStepTime = 0.0f;
        //------
    
        private Player _player;
        public PlayerState state;
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
    
        private Vector3 _velocity = Vector3.zero;
        public float speed = 40.0f;
        public static int MaxHp = 100;
        public static int Hp = MaxHp;
    
        // Rolling variables
        public float rollCoolDown = 20.0f;
        public float rollDuration = 5.0f;
        private float _timeSinceRoll = 0.0f;
        private Coroutine _rollCoroutine;
        private float _nextRollTime = 0.0f;
        private bool _isRolling = false;
        public PlayerRollStamina playerRollStamina;
        
        
        // Ammo and reload variables
        public static int Ammunition = 15;
        public AmmoBar ammoBar;
        public float reloadDuration = 3.0f;
        private Coroutine _reloadCoroutine;
        
        // Shoot variables
        public float shootCooldown = 0.2f;
        private float _nextShootTime = 0.0f;
        
        public GameObject fishProjectile;
        public Transform firePoint;
        public Transform damagePoint;
        public PlayerHealthBar hpBar;
        public HideStaminaBar h;
        
        
        [SerializeField] private SampleWave wave1;
        

    
        public LosingScreenManager losingScreenManager;
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Start is called before the first frame update
        private void Start()
        {
            Application.targetFrameRate = 60;
            state = PlayerState.Idle;
            playerRollStamina.setValue(1);
        }


        private void FixedUpdate()
        {
            PlayerMove();
        }

        // Update is called once per frame
        private void Update()
        {
            switch (state)
            {
                case PlayerState.Idle:
                    if (HasMovementInput())
                    {
                        ChangeState(PlayerState.Walking);
                    }
                    break;
                case PlayerState.Walking:
                    if (!HasMovementInput())
                    {
                        ChangeState(PlayerState.Idle);
                    }
                    break;
            } 
            PlayerInput();
            
            
            if (Input.GetMouseButtonDown(0) 
                && Ammunition > 0 
                && state != PlayerState.Reloading
                && Time.time >= _nextShootTime)
            {
                Shoot();
                _nextShootTime = Time.time + shootCooldown;
            }
            
        }

        private bool HasMovementInput()
        {
            return Mathf.Abs(Input.GetAxis("Horizontal")) > 0.01f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.01f;
        }
        
        private void ChangeState(PlayerState newState)
        {
            state = newState;
        }
        
        private void PlayerInput()
        {
            _velocity.y = Input.GetAxis("Vertical") * speed;
            _velocity.x = Input.GetAxis("Horizontal") * speed;
            _velocity = Vector2.ClampMagnitude(_velocity, speed);  
            // maxLength muss immer = speed sein, damit man diagonal nicht schenller ist
        
            if (_velocity.magnitude > 0.01 && Time.time >= _nextStepTime && state == PlayerState.Walking)
            {
                SoundFXManager.instance.PlayRandomSoundFXClip(walkClips, transform, 0.1f);
                _nextStepTime = Time.time + timeForOneStep;
            }
        
            _timeSinceRoll += Time.deltaTime;
            playerRollStamina.setValue(Mathf.Clamp(_timeSinceRoll/rollCoolDown, 0, 100));
        
        
            if (Input.GetKeyDown(KeyCode.E) && state != PlayerState.Rolling)
            {
                _animator.SetTrigger("spin");
                ChangeState(PlayerState.Spinning);
                // To-DO Spinning Weglassen oder cooldown. Wenn nicht weg dann mit Coroutine
            }
        
            if (Input.GetKeyDown(KeyCode.LeftShift) && !_isRolling && state != PlayerState.Spinning)
            {
                if (Time.time >= _nextRollTime)
                {
                    ChangeState(PlayerState.Rolling);
                    _rollCoroutine = StartCoroutine(Roll());
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift) && _isRolling)
            {
                StopRoll();
            }

            if (Input.GetKeyDown(KeyCode.R) && state != PlayerState.Reloading)
            {
                ChangeState(PlayerState.Reloading);
                _reloadCoroutine = StartCoroutine(Reload());
            }
        
        }

        private void PlayerMove()
        {
            _animator.SetFloat("speed", _velocity.magnitude);
            if (_velocity.x != 0) _spriteRenderer.flipX = _velocity.x < 0;
            _rigidbody2D.velocity = _velocity * Time.fixedDeltaTime;
        }

        private IEnumerator Roll()
        {
            h.SetActive();
            _timeSinceRoll = 0.0f;
            _isRolling = true;
            _nextRollTime = Time.time + rollCoolDown;
            _animator.SetTrigger("roll");
        
            yield return new WaitForSeconds(rollDuration);
        
            StopRoll();
        }
        
        private void StopRoll()
        {
            if (speed > 0) ChangeState(PlayerState.Walking);
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

        public bool DamagePlayer(int damage, Transform position)
        {
            if (state != PlayerState.Rolling && state != PlayerState.Dead && !_isRolling)
            {
                Hp -= damage;
                Vector3 point = (UnityEngine.Random.onUnitSphere * 0.1f);
                damagePoint.position += point;
                DamageCounterManager.Instance.InstantiateDamage(damagePoint, damage.ToString());
                _animator.SetTrigger("hit");
                hpBar.SetHealth(Hp);
                if (Hp <= 0)
                {
                    Die();
                }

                return true;
            }
            return false;
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
            
            Ammunition--;
            ammoBar.SetAmmo(Ammunition);
            if (Ammunition == 0 && state != PlayerState.Reloading)
            {
                ChangeState(PlayerState.Reloading);
                _reloadCoroutine = StartCoroutine(Reload());
            }
        }

        private IEnumerator Reload()
        {
            float startAmmo = Ammunition; 
            float targetAmmo = 15f;

            float elapsed = 0f;
            while (elapsed < reloadDuration)
            {
                elapsed += Time.deltaTime;
                float progress = Mathf.Clamp01(elapsed / reloadDuration);
                float currentAmount = Mathf.Lerp(startAmmo, targetAmmo, progress);
                ammoBar.SetAmmo(currentAmount);
                yield return null;
            }

            Ammunition = 15;
            ammoBar.SetAmmo(Ammunition);
            StopReload();
        }

        private void StopReload()
        {
            if (_reloadCoroutine != null)
            {
                StopCoroutine(_reloadCoroutine);
                _reloadCoroutine = null;
            }

            if (HasMovementInput())
            {
                ChangeState(PlayerState.Walking);
            }
            else
            {
                ChangeState(PlayerState.Idle);
            }
        }

        private void Die()
        {
            ChangeState(PlayerState.Dead);
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition;
            _animator.SetBool("isDead", true);
            losingScreenManager.ShowLosingScreen();
        }
    }
}
