using System;
using System.Collections;
using UnityEngine;

namespace Waves
{
    public class SampleWave : MonoBehaviour
    {
        public UpgradeMenu upgradeMenu;
        
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
        private int wave = 1;
        private bool isSet = false;
        public int enemyCap = 10; // siehe unten
        void Start()
        {
            activeEnemies = 0;
            wave = 1;
            SetWave();                     
            StartCoroutine(StartWave());  
            upgradeMenu = FindObjectOfType<UpgradeMenu>();
        }

        public void SetWave()
        {
            //du könntest sowas machen

            switch (wave)
            {
                case 1: //wie viele gegner in wave 1 spawnen
                {
                    numBat = 0;
                    numGrinch = 0;
                    numKrampus = 10;
                    numSnowman = 0;
                    numBoss = 0;
                    break;
                }
                case 2: //in wave 2
                {
                    numBat = 5;
                    numGrinch = 0;
                    numKrampus = 10;
                    numSnowman = 0;
                    numBoss = 0;
                    break;
                }
                case 3: //usw.
                {
                    numBat = 10;
                    numGrinch = 0;
                    numKrampus = 10;
                    numSnowman = 0;
                    numBoss = 0;
                    break;
                }
                case 4:
                {
                    numBat = 5;
                    numGrinch = 0;
                    numKrampus = 10;
                    numSnowman = 5;
                    numBoss = 0;
                    break;
                }
                case 5:
                {
                    numBat = 10;
                    numGrinch = 0;
                    numKrampus = 10;
                    numSnowman = 10;
                    numBoss = 0;
                    break;
                }
                case 6:
                {
                    numBat = 5;
                    numGrinch = 5;
                    numKrampus = 0;
                    numSnowman = 5;
                    numBoss = 0;
                    break;
                }
                case 7:
                {
                    numBat = 10;
                    numGrinch = 10;
                    numKrampus = 0;
                    numSnowman = 10;
                    numBoss = 0;
                    break;
                }
                case 8:
                {
                    numBat = 0;
                    numGrinch = 0;
                    numKrampus = 20;
                    numSnowman = 20;
                    numBoss = 0;
                    break;
                }
                case 9:
                {
                    numBat = 10;
                    numGrinch = 10;
                    numKrampus = 10;
                    numSnowman = 10;
                    numBoss = 0;
                    break;
                }
                case 10: //Boss
                {
                    numBat = 0;
                    numGrinch = 0;
                    numKrampus = 0;
                    numSnowman = 0;
                    numBoss = 1;
                    break;
                }
                default:
                {
                    numBat = 0;
                    numGrinch = 0;
                    numKrampus = 0;
                    numSnowman = 0;
                    numBoss = 0;
                    break;
                }
            
            }
            activeEnemies = numBat+numGrinch+numSnowman+numKrampus+numBoss;
        }

        public IEnumerator StartWave()
        {
            yield return new WaitForSeconds(3);
            enemies = new GameObject[numBat+numGrinch+numSnowman+numKrampus+numBoss];

        
            int index = 0;
            int count = 0;

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
            //spawnt so nicht jede Runde ein Boss? ne nur wenn numBoss >= 1 ist
            while (count < numBoss)
            {
                enemies[index] = Instantiate(boss);
                count++;
                index++;
            }
            wave += 1;
            enemyCap += 2; //maybe something like numGrinch+=2
            isSet = false;
            
            //neue Wave nach 90 Sek
            yield return new WaitForSeconds(90);
            isSet = true;
        }
    
    
        // Update is called once per frame
        void Update()
        {
            if (activeEnemies == 0 && isSet == false)
            {
                isSet = true;
                upgradeMenu.ShowMenu();
            }
        }
    }
}
