using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinSpeedBehaviour : StateMachineBehaviour
{
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var player = animator.GetComponent<Player>();
        player.speed -= 10.0f;
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       var player = animator.GetComponent<Player>();
       player.speed += 10.0f;
    }
}
