using PlayerScripts;
using UnityEngine;

namespace EnemyScripts
{
    public class Snowball : MonoBehaviour
    {
        public float speed = 2.5f;
        public int damage = 5;
        private Vector3 _direction;
    
    
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            transform.position += _direction * (speed * Time.deltaTime);
        }


        public void SetDirection(Vector3 dir)
        {
            _direction = dir.normalized;
        }

    

        private void OnTriggerEnter2D(Collider2D other)
        {
            GameObject player = other.gameObject;
            Player script = player.GetComponent<Player>();
            if(script.DamagePlayer(damage, transform)) Destroy(gameObject);
        }


        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}
