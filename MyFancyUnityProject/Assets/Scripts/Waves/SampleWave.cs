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
    public int count = 0;
    public int enemyCap = 10; // siehe unten
    void Start()
    {
        activeEnemies = 0;
        StartWave();
    }

    public IEnumerator StartWave()
    {
        yield return new WaitForSeconds(3);
        enemies = new GameObject[numBat+numGrinch+numSnowman+numKrampus+numBoss];

        
        int index = 0;
        
        
        //Test
        //Wave 1


            while (count < enemyCap)
            {
                Instantiate(krampus);
                count++;
                index++;
                activeEnemies++;
            }

            

            while (count < enemyCap)
            {
                enemies[index] = Instantiate(snowman);
                count++;
                index++;
            }

            

            while (count < enemyCap)
            {
                enemies[index] = Instantiate(bat);
                count++;
                index++;
            }

            

            while (count < enemyCap)
            {
                enemies[index] = Instantiate(grinch);
                count++;
                index++;
            }

            

            //spawnt so nicht jede Runde ein Boss?
            //while (count < numBoss)
            //{
           //     enemies[index] = Instantiate(boss);
           //     count++;
           //     index++;
            //}

            if (wave == 10)
            {
                
            }
            
            if (activeEnemies == 0)
            {
                StartCoroutine(StartWave());
                enemyCap = +2;
                wave = +1;
            }
            
            
             //maybe something like numGrinch+=2
        }
    

    public void Bosswave()
    {
        if (wave == 10)
        {
            wave = 0;
            Instantiate(boss);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
