using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadyMoveState : ShadyGroundedState
{
    public ShadyMoveState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_Shady shady) : base(enemyBase, enemyStateMachine, enemyAnimBoolName, shady)
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

        shady.setVelocity(shady.moveSpeed * shady.facingDir, shady.rb.velocity.y);
        //Debug.Log("Skeleton is moving");

        if (shady.IsWallDetected())
        {

            shady.Flip();
            enemyStateMachine.changeState(shady.shadyIdleState);

            Debug.Log("Wall detected ");

        }

        if (!shady.IsGroundDetected())
        {
            shady.Flip();
            enemyStateMachine.changeState(shady.shadyIdleState);
            Debug.Log(" no ground");
        }
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exiting Move State");
    }
}
