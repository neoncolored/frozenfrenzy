using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleWave : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] enemies;
    [SerializeField] private GameObject krampus;
    [SerializeField] private GameObject snowman;
    [SerializeField] private GameObject grinch;
    [SerializeField] private GameObject bat;
    [SerializeField] private GameObject boss;
    [NonSerialized] public static int activeEnemies;
    public int numKrampus;
    public int numBat;
    public int numSnowman;
    public int numGrinch;
    public int numBoss;
    public int wave = 0;
    public int enemyCap = 10; // siehe unten
    void Start()
    {
        activeEnemies = numBat+numGrinch+numSnowman+numKrampus+numBoss;
    }

    public IEnumerator StartWave()
    {
        yield return new WaitForSeconds(3);
        enemies = new GameObject[numBat+numGrinch+numSnowman+numKrampus+numBoss];

        int count = 0;
        int index = 0;
        
        while (count < numKrampus)
        {
            enemies[index] = Instantiate(krampus);
            count++;
            index++;
        }

        count = 0;
        
        while (count < numSnowman)
        {
            enemies[index] = Instantiate(snowman);
            count++;
            index++;
        }
        
        count = 0;
        
        while (count < numBat)
        {
            enemies[index] = Instantiate(bat);
            count++;
            index++;
        }
        
        count = 0;
        
        while (count < numGrinch)
        {
            enemies[index] = Instantiate(grinch);
            count++;
            index++;
        }
        
        count = 0;
        
        while (count < numBoss)
        {
            enemies[index] = Instantiate(boss);
            count++;
            index++;
        }
        
        activeEnemies = numBat+numGrinch+numSnowman+numKrampus+numBoss;
        wave = +1;
        enemyCap = +2; //maybe something like numGrinch+=2
    }
    
    // Update is called once per frame
    void Update()
    {
        if (activeEnemies == 0)
        {
            activeEnemies = numBat+numGrinch+numSnowman+numKrampus+numBoss;
            StartCoroutine(StartWave());
        }
    }
}
