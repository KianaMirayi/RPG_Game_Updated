using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherIdleState : ArcherGroundedState
{
    public ArcherIdleState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_Archer archerEnemy) : base(enemyBase, enemyStateMachine, enemyAnimBoolName, archerEnemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = archerEnemy.idleTime; ;

        //Debug.Log("Entered Idle State");
    }


    public override void Update()
    {
        base.Update();
        //Debug.Log("Skeleton is in idle");

        if (stateTimer < 0)
        {
            enemyStateMachine.changeState(archerEnemy.archerMoveState);
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
