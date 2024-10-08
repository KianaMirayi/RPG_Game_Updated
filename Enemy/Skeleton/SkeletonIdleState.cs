using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : SkeletonGroundState
{
    public SkeletonIdleState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_Skeleton enemy) : base(enemyBase, enemyStateMachine, enemyAnimBoolName, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = skeletonEnemy.idleTime; ;

        //Debug.Log("Entered Idle State");
    }


    public override void Update()
    {
        base.Update();
        //Debug.Log("Skeleton is in idle");

        if (stateTimer < 0)
        {
            enemyStateMachine.changeState(skeletonEnemy.skeletonMoveState);
            //Debug.Log("Switching to Move state");
        }
        
        
    }
    public override void Exit()
    {
        base.Exit();

        AudioManager.instance.PlaySfx(46, skeletonEnemy.transform); //For Test
        //Debug.Log("Exiting Idle State");
    }
}
