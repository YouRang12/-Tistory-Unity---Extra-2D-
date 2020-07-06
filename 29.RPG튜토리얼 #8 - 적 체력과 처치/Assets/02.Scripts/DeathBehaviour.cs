using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBehaviour : StateMachineBehaviour
{
    float timePassed;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // hitBox제거(Player부터 타겟인식x)
        Destroy(animator.transform.GetChild(0).gameObject);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timePassed += Time.deltaTime;
        if (timePassed >= 5)
        {
            // 스켈레톤 삭제
            Destroy(animator.gameObject);
        }
    }
}
