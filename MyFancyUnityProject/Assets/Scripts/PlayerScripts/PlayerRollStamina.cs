using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRollStamina : MonoBehaviour
{
    public Slider staminaBar;
    
    
    void Start(){
        staminaBar = GetComponent<Slider>();
        staminaBar.maxValue = 1;
        staminaBar.value = 1;
        
    }
    
    public void setValue(float amount)
    {
        staminaBar.value = amount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
