using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 8.0f;
    public GameObject player;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        ResetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayer(player);
    }


    public void ResetPosition()
    {
        transform.position = new Vector2(Random.Range(-1.0f, 6.0f), Random.Range(-2.0f, 1.0f));
    }

    public void MoveTowardsPlayer(GameObject target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        
    }
}