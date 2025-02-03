using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageCounterManager : MonoBehaviour
{
    public static DamageCounterManager Instance;
    
    private void Awake()
    {
        Instance = this;
        
    }
    public GameObject damageTextPrefab;

    public void InstantiateDamage(Transform positionToAttachTo, string damageAmount)
    {
        GameObject damageTextInstance = Instantiate(damageTextPrefab, positionToAttachTo);
        damageTextInstance.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(damageAmount);
    }
}
