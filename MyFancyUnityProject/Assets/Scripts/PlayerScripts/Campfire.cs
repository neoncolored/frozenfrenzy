using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Campfire : MonoBehaviour
{

    public float hpPerTick = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        GameObject player = other.gameObject;
        if (player.TryGetComponent<Player>(out Player p))
        {
            Player script = player.GetComponent<Player>();
            script.hpBar.healthBar.value += hpPerTick;
        }
        
        
    }
}
