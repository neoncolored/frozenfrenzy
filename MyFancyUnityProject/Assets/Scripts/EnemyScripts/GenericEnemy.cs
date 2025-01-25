using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GenericEnemy : MonoBehaviour
{
    
    
    public float speed;
    public float range;
    public float attackSpeed;
    public float attackDuration;
    public int damage;
    public int hp;
    public static int MaxHp;
    public int deathDuration;
    public int hurtDuration;
    public GenericHealthBar genericHealthBar;
    

    private void Start()
    {
        MaxHp = hp;
        genericHealthBar.genericHealthBar.maxValue = MaxHp;
        genericHealthBar.genericHealthBar.value = MaxHp;
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
            
            //do for each enemy sadly
        }
        else
        {
            if (genericScript.GetType() == typeof(Krampus))
            {
                Krampus krampus = GetComponent<Krampus>();
                StartCoroutine(krampus.PlayHurtAnimation());
            }
        }
    }
    
    private void ResetPosition()
    {
        transform.position = new Vector2(Random.Range(-2.4f, 3.8f), Random.Range(-3.25f, 3.05f));
    }

    public void PlayDeathAnimation()
    {
        //nothing here!
        Debug.Log("in generic");
    }
    

}
