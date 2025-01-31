using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Campfire : MonoBehaviour
{

    public float hpPerTick = 0.1f;
    public AudioClip campfireSound;
    private bool isHealing = false;

    private Player _player;

    private void Start()
    {
        SoundFXManager.instance.PlaySoundFXClip(campfireSound, transform, 0.2f, true, true);
    }

    private void FixedUpdate()
    {
        
        if (isHealing)
        {
            Heal(_player);
        }
    }

    private void Heal(Player p)
    {
        _player.hpBar.healthBar.value += hpPerTick;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        GameObject player = other.gameObject;
        if (player.TryGetComponent<Player>(out Player p))
        {
            _player = player.GetComponent<Player>();
            isHealing = true;
        } 
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GameObject player = other.gameObject;
        if (player.TryGetComponent<Player>(out Player p))
        {
            _player = player.GetComponent<Player>();
            isHealing = false;
        }
    }
}
