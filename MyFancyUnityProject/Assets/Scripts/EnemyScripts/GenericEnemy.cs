using System;
using System.Collections;
using EnemyScripts;
using UnityEngine;
using Random = UnityEngine.Random;
using PlayerScripts;
using Waves;

namespace EnemyScripts
{
    public class GenericEnemy : MonoBehaviour
    {
    
        public GameObject player;
        public float speed;
        public float range;
        public float attackSpeed;
        public float attackDuration;
        public int damage;
        public int hp;
        [NonSerialized] public int maxHp;
        public float deathDuration;
        public float hurtDuration;
        public GenericHealthBar genericHealthBar;
        public Transform damageSpawnPoint;
        private bool _golemPhaseFour = false;
        private bool _golemPhaseThree = false;
        private bool _golemPhaseTwo = false;
        private bool _isStunned = false;
        
        
        protected Rigidbody2D Rb;
        public bool IsStunned => _isStunned;


        protected virtual void Awake()
        {
            Rb = GetComponentInChildren<Rigidbody2D>();
        }

        private void Start()
        {
            player = GameObject.Find("Player");
            maxHp = hp;
            genericHealthBar.genericHealthBar.maxValue = maxHp;
            genericHealthBar.genericHealthBar.value = hp;
            ResetPosition();
        }
    
    
        public void DamageEnemy(int amount)
        {
            GenericEnemy genericScript = GetComponent<GenericEnemy>();
            bool isInvulnerable = false;
            
            if (genericScript.GetType() == typeof(Golem))
            {
                Golem golem = GetComponent<Golem>();
                isInvulnerable = golem.isInvulnerable;
            }

            if (!isInvulnerable)
            {
                hp -= amount;
                Vector3 point = (UnityEngine.Random.onUnitSphere * 0.1f);
                Transform temp = damageSpawnPoint;
                damageSpawnPoint.position += point;
                DamageCounterManager.Instance.InstantiateDamage(damageSpawnPoint, amount.ToString());
                genericHealthBar.SetHealth(hp);
                damageSpawnPoint = temp;
            }
            
            if (hp <= 0)
            {
                SampleWave.activeEnemies--;
            
                if (genericScript.GetType() == typeof(Krampus))
                {
                    Krampus krampus = GetComponent<Krampus>();
                    GetComponent<BoxCollider2D>().enabled = false;
                    StartCoroutine(krampus.PlayDeathAnimation());
                }

                if (genericScript.GetType() == typeof(EvilSnowman))
                {
                    EvilSnowman evilSnowman = GetComponent<EvilSnowman>();
                    GetComponent<BoxCollider2D>().enabled = false;
                    StartCoroutine(evilSnowman.PlayDeathAnimation());
                }
            
                if (genericScript.GetType() == typeof(Grinch))
                {
                    Grinch grinch = GetComponent<Grinch>();
                    GetComponent<BoxCollider2D>().enabled = false;
                    StartCoroutine(grinch.PlayDeathAnimation());
                }
            
                if (genericScript.GetType() == typeof(Bat))
                {
                    Bat bat = GetComponent<Bat>();
                    GetComponent<BoxCollider2D>().enabled = false;
                    StartCoroutine(bat.PlayDeathAnimation());
                }
            
                if (genericScript.GetType() == typeof(Golem))
                {
                    Golem golem = GetComponent<Golem>();
                    GetComponent<BoxCollider2D>().enabled = false;
                    StartCoroutine(golem.PlayDeathAnimation());
                
                }
            
                //do for each enemy sadly
            }
            else
            {
                if (genericScript.GetType() == typeof(Krampus))
                {
                    Krampus krampus = GetComponent<Krampus>();
                    StartCoroutine(krampus.PlayHurtAnimation());
                }
            
                if (genericScript.GetType() == typeof(EvilSnowman))
                {
                    EvilSnowman evilSnowman = GetComponent<EvilSnowman>();
                    StartCoroutine(evilSnowman.PlayHurtAnimation());
                }
            
                if (genericScript.GetType() == typeof(Grinch))
                {
                    Grinch grinch = GetComponent<Grinch>();
                    StartCoroutine(grinch.PlayHurtAnimation());
                }
            
                if (genericScript.GetType() == typeof(Bat))
                {
                    Bat bat = GetComponent<Bat>();
                    StartCoroutine(bat.PlayHurtAnimation());
                }
            
                if (genericScript.GetType() == typeof(Golem))
                {
                    Golem golem = GetComponent<Golem>();
                    StartCoroutine(golem.PlayHurtAnimation());
                    if (hp <= 300)
                    {
                        if (!_golemPhaseTwo)
                        {
                            golem.state = Golem.EnemyState.PHASETWO;
                            _golemPhaseTwo = true;
                            //hier sound für phasechange
                            
                            //
                        }

                        
                    }

                    if (hp <= 200)
                    {
                        if (!_golemPhaseThree)
                        {
                            golem.state = Golem.EnemyState.PHASETHREE;
                            golem.specialAttackSpeed = 2.0f;
                            _golemPhaseThree = true;
                            //hier sound für phasechange
                            
                            //
                        }

                        
                    }

                    if (hp <= 100)
                    {
                        if (!_golemPhaseFour)
                        {
                            golem.state = Golem.EnemyState.PHASEFOUR;
                            golem.specialAttackSpeed = 0.5f;
                            golem.nextSpecialAttackTime = Time.time + 1f;
                            _golemPhaseFour = true;
                            //hier sound für phasechange
                            
                            //
                        }
                        
                    }
                }
            }
        }
    
        public void ResetPosition()
        {
            transform.position = new Vector2(Random.Range(-2.4f, 3.8f), Random.Range(-3.25f, 3.05f));
        }
        
        public void Stun(float duration)
        {
            StartCoroutine(StunRoutine(duration));
        }

        private IEnumerator StunRoutine(float duration)
        {
            _isStunned = true;
            yield return new WaitForSeconds(duration);
            _isStunned = false;
        }
    }
}
