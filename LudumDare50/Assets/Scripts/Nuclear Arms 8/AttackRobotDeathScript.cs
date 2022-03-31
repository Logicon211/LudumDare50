using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRobotDeathScript : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AttackBotController controller = animator.gameObject.GetComponentInParent<AttackBotController>();
        if (controller != null) Destroy(controller.gameObject, stateInfo.length+1);
        Destroy(animator.gameObject, stateInfo.length+1);
    }
}