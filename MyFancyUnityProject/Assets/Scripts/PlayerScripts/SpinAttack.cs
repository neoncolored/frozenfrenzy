using System.Collections;
using System.Collections.Generic;
using EnemyScripts;
using UnityEngine;

namespace PlayerScripts
{
    public class SpinAttack : MonoBehaviour
    {
    public static int Damage = 10;
    private Vector3 _direction;

    [SerializeField] private Collider2D spinCollider;

    public void SetDirection(Vector3 direction)
    {
        _direction = direction.normalized;
    }

    public void ActivateCollider()
    {
        if (spinCollider != null)
            spinCollider.enabled = true;
    }

    public void DeactivateCollider()
    {
        if (spinCollider != null)
            spinCollider.enabled = false;
    }

    private void Awake()
    {
        if (spinCollider == null)
        {
            spinCollider = GetComponent<Collider2D>();
        }
        DeactivateCollider();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Enemy"))
        {
            GenericEnemy enemy = other.GetComponent<GenericEnemy>(); 
            if (enemy != null)
            {
                enemy.DamageEnemy(Damage);
                enemy.Stun(0.5f);
            }
        }
    } 
    }
}
