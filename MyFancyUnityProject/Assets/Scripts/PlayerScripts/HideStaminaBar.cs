using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HideStaminaBar : MonoBehaviour
{
    private Canvas _canvas;
    public Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
    }

    public IEnumerator SetInactive()
    {
        yield return new WaitForSeconds(player.rollCoolDown-player.rollDuration);
        yield return _canvas.enabled = false;
    }
    
    
    public void SetActive()
    {
        StopCoroutine(SetInactive());
        _canvas.enabled = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
