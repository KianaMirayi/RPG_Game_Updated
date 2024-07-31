using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadyIdleState : ShadyGroundedState
{
    public ShadyIdleState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_Shady shady) : base(enemyBase, enemyStateMachine, enemyAnimBoolName, shady)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = shady.idleTime; ;

        //Debug.Log("Entered Idle State");
    }


    public override void Update()
    {
        base.Update();
        //Debug.Log("Skeleton is in idle");

        if (stateTimer < 0)
        {
            enemyStateMachine.changeState(shady.shadyMoveState);
            //Debug.Log("Switching to Move state");
        }


    }
    public override void Exit()
    {
        base.Exit();

        //AudioManager.instance.PlaySfx(19, archerEnemy.transform); //For Test
        //Debug.Log("Exiting Idle State");
    }
}
