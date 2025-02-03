using UnityEngine;

namespace PlayerScripts
{
    public class PlayerCamera : MonoBehaviour
    {
        public GameObject player;
    
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.Find("Player"); // The player
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, - 10);
        }
    }
}
