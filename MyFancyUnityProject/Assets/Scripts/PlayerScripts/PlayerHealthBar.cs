using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider healthBar;
    private float maxHealth = Player.maxHp;
    
    
    void Start(){
        healthBar =GetComponent<Slider>();
        healthBar.maxValue = maxHealth;
        healthBar.value= maxHealth;
        
    }
    
    public void SetHealth(int hp){
        healthBar.value= hp;
    }
        

    // Update is called once per frame
    void Update()
    {
        
    }
}
