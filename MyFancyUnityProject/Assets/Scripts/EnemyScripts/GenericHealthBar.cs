using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenericHealthBar : MonoBehaviour
{
    
    public Slider genericHealthBar;
    private float maxHealth = GenericEnemy.MaxHp;
    
    
    void Start(){
        genericHealthBar = GetComponent<Slider>();
        genericHealthBar.value= maxHealth;
        
    }
    
    public void SetHealth(int hp){
        genericHealthBar.value= hp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
