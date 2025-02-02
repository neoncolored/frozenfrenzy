using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageCounterManager : MonoBehaviour
{
    public void DestroyParent()
    {
        GameObject parent = gameObject.transform.parent.gameObject;
        Destroy(parent);
    }


    public GameObject damageTextPrefab, enemyInstance;
    public string textToDisplay;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X)) InstantiateDamage();
    }

    public void InstantiateDamage()
    {
        GameObject damageTextInstance = Instantiate(damageTextPrefab, enemyInstance.transform);
        damageTextInstance.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(textToDisplay);
    }
}
