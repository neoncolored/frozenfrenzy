using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollSpeedBehaviour : StateMachineBehaviour
{
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player player = animator.GetComponent<Player>();
        if (player != null)
        {
            player.speed += 20.0f;
        }
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player player = animator.GetComponent<Player>();
        if (player != null)
        {
            player.speed -= 20.0f;
        }
    }
}
