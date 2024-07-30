using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMoveState : ArcherGroundedState
{
    public ArcherMoveState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_Archer archerEnemy) : base(enemyBase, enemyStateMachine, enemyAnimBoolName, archerEnemy)
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

        archerEnemy.setVelocity(archerEnemy.moveSpeed * archerEnemy.facingDir, archerEnemy.rb.velocity.y);
        //Debug.Log("Skeleton is moving");

        if (archerEnemy.IsWallDetected())
        {

            archerEnemy.Flip();
            enemyStateMachine.changeState(archerEnemy.archerIdleState);

            Debug.Log("Wall detected ");

        }

        if (!archerEnemy.IsGroundDetected())
        {
            archerEnemy.Flip();
            enemyStateMachine.changeState(archerEnemy.archerIdleState);
            Debug.Log(" no ground");
        }
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exiting Move State");
    }
}
