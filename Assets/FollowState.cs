using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : StateMachineBehaviour
{   
   Transform enemyTransform;
    Boss boss;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        boss = animator.GetComponent<Boss>();
        enemyTransform = animator.GetComponent<Transform>();
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
       
        if(Vector2.Distance(boss.player.position, enemyTransform.position) > 1f)
        enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, boss.player.position, Time.deltaTime * boss.speed);
        else
        {
          animator.SetBool("IsBack", false);
          animator.SetBool("IsFollow", false);  
        }
        boss.DirectionEnemy(boss.player.position.x, enemyTransform.position.x);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
