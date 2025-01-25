using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenericHealthBar : MonoBehaviour
{
    
    public Slider genericHealthBar;
    private float _maxHealth;
    public GenericEnemy g;
    
    
    void Start()
    {
        _maxHealth = g.maxHp;
        genericHealthBar = GetComponent<Slider>();
        genericHealthBar.value= _maxHealth;
        
    }
    
    public void SetHealth(int hp){
        genericHealthBar.value= hp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
