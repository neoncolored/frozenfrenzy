using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenericEnemy : MonoBehaviour
{
    
    
    public float speed;
    public float range;
    public float attackSpeed;
    public float attackDuration;
    public int damage;
    public int hp;
    public int maxHp;
    public float deathDuration;
    public float hurtDuration;
    public GenericHealthBar genericHealthBar;
    

    private void Start()
    {
        maxHp = hp;
        genericHealthBar.genericHealthBar.maxValue = hp;
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
        }
    }
    
    private void ResetPosition()
    {
        transform.position = new Vector2(Random.Range(-2.4f, 3.8f), Random.Range(-3.25f, 3.05f));
    }

    

}
