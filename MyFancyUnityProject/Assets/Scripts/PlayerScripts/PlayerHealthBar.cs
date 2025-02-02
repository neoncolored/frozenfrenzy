using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider healthBar;
    public GameObject hpText;
    private float maxHealth = Player.maxHp;
    private TextMeshProUGUI hpTextTMP;
    private bool set = false;
    
    
    void Start(){
        healthBar =GetComponent<Slider>();
        healthBar.maxValue = maxHealth;
        healthBar.value= maxHealth;
        hpTextTMP = hpText.GetComponent<TextMeshProUGUI>();
        hpTextTMP.SetText("100/100");

    }
    
    public void SetHealth(float hp)
    {
        healthBar.value = hp;
        Player.hp = (int)Math.Round(hp);
        hpTextTMP.SetText((int)Math.Round(hp)+"/100");
    }
        

    // Update is called once per frame
    void Update()
    {
        
    }
}
