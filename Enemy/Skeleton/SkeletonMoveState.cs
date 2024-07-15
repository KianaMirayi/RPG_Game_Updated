using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SkeletonMoveState : SkeletonGroundState
{
    public SkeletonMoveState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_Skeleton enemy) : base(enemyBase, enemyStateMachine, enemyAnimBoolName, enemy)
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

        skeletonEnemy.setVelocity(skeletonEnemy.moveSpeed * skeletonEnemy.facingDir, skeletonEnemy.rb.velocity.y);
        //Debug.Log("Skeleton is moving");

        if (skeletonEnemy.IsWallDetected())
        {
            
            skeletonEnemy.Flip();
            enemyStateMachine.changeState(skeletonEnemy.skeIdleState);

            Debug.Log("Wall detected ");
            
        }

        if (!skeletonEnemy.IsGroundDetected())
        {
            skeletonEnemy.Flip();
            enemyStateMachine.changeState(skeletonEnemy.skeIdleState);
            Debug.Log(" no ground");
        }
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exiting Move State");
    }
}
