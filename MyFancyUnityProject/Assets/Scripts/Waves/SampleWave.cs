using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleWave : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] enemies;
    void Start()
    {

    }

    public IEnumerator StartWave()
    {
        yield return new WaitForSeconds(3);
        for (int i = 0; i < enemies.Length; i++)
        {
            Instantiate(enemies[i]);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
