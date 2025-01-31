using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

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
        hp -= amount;
        genericHealthBar.SetHealth(hp);
        GenericEnemy genericScript = GetComponent<GenericEnemy>();
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
            
            //do for each enemy sadly
        }
        else
        {
            if (genericScript.GetType() == typeof(Krampus))
            {
                Debug.Log("here");
                Krampus krampus = GetComponent<Krampus>();
                StartCoroutine(krampus.PlayHurtAnimation());
                Debug.Log("here2");
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
        }
    }
    
    public void ResetPosition()
    {
        transform.position = new Vector2(Random.Range(-2.4f, 3.8f), Random.Range(-3.25f, 3.05f));
    }

    

}
