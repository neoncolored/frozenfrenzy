using UnityEngine;
using UnityEngine.UI;

namespace EnemyScripts
{
    public class GenericHealthBar : MonoBehaviour
    {
    
        public Slider genericHealthBar;
        private float _maxHealth;
        public GenericEnemy g;
    
    
        void Start()
        {
            _maxHealth = g.maxHp;
            genericHealthBar = GetComponent<Slider>();
            genericHealthBar.maxValue = _maxHealth;
            genericHealthBar.value= _maxHealth;
        
        }
    
        public void SetHealth(int hp){
            genericHealthBar.value= hp;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
