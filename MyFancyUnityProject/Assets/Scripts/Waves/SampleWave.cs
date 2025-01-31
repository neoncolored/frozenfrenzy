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
    [NonSerialized] public static int activeEnemies;
    public int numKrampus;
    public int numBat;
    public int numSnowman;
    public int numGrinch;
    public int wave = 0;
    public int enemyCap = 10; // siehe unten
    void Start()
    {
        activeEnemies = numBat+numGrinch+numSnowman+numKrampus;
    }

    public IEnumerator StartWave()
    {
        yield return new WaitForSeconds(3);
        enemies = new GameObject[numBat+numGrinch+numSnowman+numKrampus];

        int count = 0;
        
        while (count < numKrampus)
        {
            enemies[count] = Instantiate(krampus);
            count++;
        }

        count = 0;
        
        while (count < numSnowman)
        {
            enemies[count] = Instantiate(snowman);
            count++;
        }
        
        count = 0;
        
        while (count < numBat)
        {
            enemies[count] = Instantiate(bat);
            count++;
        }
        
        count = 0;
        
        while (count < numGrinch)
        {
            enemies[count] = Instantiate(grinch);
            count++;
        }
        
        activeEnemies = numBat+numGrinch+numSnowman+numKrampus;
        wave = +1;
        enemyCap = +2; //maybe something like numGrinch+=2
    }
    
    // Update is called once per frame
    void Update()
    {
        if (activeEnemies == 0)
        {
            activeEnemies = numBat + numGrinch + numSnowman + numKrampus;
            StartCoroutine(StartWave());
        }
    }
}
