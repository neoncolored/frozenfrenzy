using UnityEngine;

namespace PlayerScripts
{
    public class RollSpeedBehaviour : StateMachineBehaviour
    {
    
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var player = animator.GetComponent<Player>();
            if (player != null)
            {
                player.speed += 30.0f;
            }
        }
    
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var player = animator.GetComponent<Player>();
            if (player != null)
            {
                player.speed -= 30.0f;
            }
        }
    }
}
