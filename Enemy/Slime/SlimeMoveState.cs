using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMoveState : SlimeGroundedState
{
    public SlimeMoveState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_Slime _slimeEnemy) : base(enemyBase, enemyStateMachine, enemyAnimBoolName, _slimeEnemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Entered Move State");
    }


    public override void Update()
    {
        base.Update();

        slimeEnemy.setVelocity(slimeEnemy.moveSpeed * slimeEnemy.facingDir, slimeEnemy.rb.velocity.y);
        //Debug.Log("Skeleton is moving");

        if (slimeEnemy.IsWallDetected())
        {

            slimeEnemy.Flip();
            enemyStateMachine.changeState(slimeEnemy.slimeIdleState);

            Debug.Log("Wall detected ");

        }

        if (!slimeEnemy.IsGroundDetected())
        {
            slimeEnemy.Flip();
            enemyStateMachine.changeState(slimeEnemy.slimeIdleState);
            Debug.Log(" no ground");
        }
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exiting Move State");
    }
}
