using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MonoBehaviour
{
    public float speed = 2.5f;
    public int damage = 5;
    private Vector3 _direction;
    public Transform damageSpawn;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _direction * (speed * Time.deltaTime);
    }


    public void SetDirection(Vector3 dir)
    {
        _direction = dir.normalized;
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject player = other.gameObject;
        if(player.TryGetComponent<Player>(out Player p))
        {
            p.DamagePlayer(damage, damageSpawn);
        }
        Destroy(gameObject);
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
