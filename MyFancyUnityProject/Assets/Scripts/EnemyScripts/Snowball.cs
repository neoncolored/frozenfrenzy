using Managers;
using PlayerScripts;
using UnityEngine;

namespace EnemyScripts
{
    
    public class Snowball : MonoBehaviour
    {
        public float speed = 2.5f;
        public int damage = 5;
        private Vector3 _direction;
        public AudioClip SnowHit;
    
    
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
            if (player.TryGetComponent(out Player p))
            {
                if (p.DamagePlayer(damage, transform))
                {
                    SoundFXManager.instance.PlaySoundFXClip(SnowHit, transform, 0.2f, false, false);
                    Destroy(gameObject);
                }
            }

            
        }


        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}
