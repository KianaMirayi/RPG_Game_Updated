using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeIdleState : SlimeGroundedState
{
    public SlimeIdleState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_Slime _slimeEnemy) : base(enemyBase, enemyStateMachine, enemyAnimBoolName, _slimeEnemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = slimeEnemy.idleTime; ;

        //Debug.Log("Entered Idle State");
    }


    public override void Update()
    {
        base.Update();
        //Debug.Log("Skeleton is in idle");

        if (stateTimer < 0)
        {
            enemyStateMachine.changeState(slimeEnemy.slimeMoveState);
            //Debug.Log("Switching to Move state");
        }


    }
    public override void Exit()
    {
        base.Exit();

        //AudioManager.instance.PlaySfx(19, slimeEnemy.transform); //For Test
        //Debug.Log("Exiting Idle State");
    }
}
