using System;
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
                damageSpawnPoint.position += point;
                DamageCounterManager.Instance.InstantiateDamage(damageSpawnPoint, damage.ToString());
                genericHealthBar.SetHealth(hp);
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
                        golem.state = Golem.EnemyState.PHASETWO;
                    }

                    if (hp <= 200)
                    {
                        golem.state = Golem.EnemyState.PHASETHREE;
                        golem.specialAttackSpeed = 2.0f;
                    }

                    if (hp <= 100)
                    {
                        golem.state = Golem.EnemyState.PHASEFOUR;
                        golem.specialAttackSpeed = 0.5f;
                        if (!_golemPhaseFour)
                        {
                            golem.nextSpecialAttackTime = Time.time + 1f;
                            _golemPhaseFour = true;
                        }
                        
                    }
                }
            }
        }
    
        public void ResetPosition()
        {
            transform.position = new Vector2(Random.Range(-2.4f, 3.8f), Random.Range(-3.25f, 3.05f));
        }

    

    }
}
