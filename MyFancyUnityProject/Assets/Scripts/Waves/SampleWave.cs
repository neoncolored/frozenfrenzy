using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleWave : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] enemies;
    [NonSerialized] public static int activeEnemies;
    void Start()
    {
        activeEnemies = 0;
    }

    public IEnumerator StartWave()
    {
        yield return new WaitForSeconds(3);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = Instantiate(enemies[i]);
        }

        activeEnemies = enemies.Length;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
