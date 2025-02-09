using System.Collections;
using Managers;
using PlayerScripts;
using UnityEngine;

namespace EnemyScripts
{
    public class Grinch : GenericEnemy
    {
        private GenericEnemy _genericEnemy2;
    

        private Player playerScript;
        private Coroutine _hurtCoroutine;
        private Animator _animator;
        private Vector3 _velocity = Vector3.zero;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody2D;
        private bool isDead = false;
        public AudioClip grinchSpawn;
        public AudioClip grinchDeath;
        public AudioClip grinchHit;
    
    
        private Coroutine _attackCoroutine;
        private float _nextAttackTime = 0.0f;
    

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = this.GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        
        
        }

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            playerScript = player.GetComponent<Player>();
            maxHp = hp;
            genericHealthBar.genericHealthBar.maxValue = maxHp;
            genericHealthBar.genericHealthBar.value = maxHp;
            ResetPosition();
            SoundFXManager.instance.PlaySoundFXClip(grinchSpawn, transform, 0.1f, false, false);

        }

        private void FixedUpdate()
        {
            if (!IsStunned)
            {
                MoveTowardsPlayer(player);    
            }
            else
            {
                if (_rigidbody2D != null)
                {
                    _rigidbody2D.velocity = Vector2.zero;    
                }
            }
        }
    

        public void MoveTowardsPlayer(GameObject target)
        {
            if (!isDead)
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
            isDead = true;
            
            SoundFXManager.instance.PlaySoundFXClip(grinchDeath, transform, 0.1f, false, false);
            
            yield return new WaitForSeconds(deathDuration);
            Destroy(gameObject);
        }
    
        public  IEnumerator PlayHurtAnimation()
        {
            _animator.SetTrigger("hurt");
            SoundFXManager.instance.PlaySoundFXClip(grinchHit, transform, 0.1f, false, false);
            yield return new WaitForSeconds(hurtDuration);
            _animator.ResetTrigger("hurt");
        }

        public IEnumerator AttackPlayer(GameObject target, Vector3 direction)
        {
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
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }
        
            _animator.ResetTrigger("attack");
        }
    }
}
