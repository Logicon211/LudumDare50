using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDeathAnimation : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnemyControllerBase controller = animator.gameObject.GetComponentInParent<EnemyControllerBase>();
        if (controller != null) {
            BearAttack bearAttack = controller.gameObject.GetComponent<BearAttack>();
            if(bearAttack != null) {
                bearAttack.enabled = false;
            }
            Destroy(controller.gameObject, stateInfo.length);
        }
        Destroy(animator.gameObject, stateInfo.length);
    }
}
