using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 2.5f;
    public int damage = 5;
    private Vector3 _direction;
    [SerializeField] private AudioClip[] hitClips;
    
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
        SoundFXManager.instance.PlayRandomSoundFXClipWithRandomPitch(hitClips, transform, 0.3f);
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        GameObject enemy = other.gameObject;
        if (enemy.TryGetComponent<GenericEnemy>(out GenericEnemy e))
        {
            GenericEnemy script = enemy.GetComponent<GenericEnemy>();
            script.DamageEnemy(5);
            SoundFXManager.instance.PlayRandomSoundFXClipWithRandomPitch(hitClips, transform, 0.3f);
            Destroy(gameObject); 
        }

        
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
