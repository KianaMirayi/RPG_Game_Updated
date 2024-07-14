using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDeadState : EnemyState
{
    public Enemy_Skeleton skeletonEnemy;
    public SkeletonDeadState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_Skeleton _skeletonEnemy) : base(enemyBase, enemyStateMachine, enemyAnimBoolName)
    {
        skeletonEnemy = _skeletonEnemy;
    }

    public override void Enter()
    {
        base.Enter();

        skeletonEnemy.anim.SetBool(skeletonEnemy.LastAnimBoolName, true);
        skeletonEnemy.anim.speed = 0;
        skeletonEnemy.cd.enabled = false;

        stateTimer = 0.1f;

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > 0)
        {
            skeletonEnemy.rb.velocity = new Vector2(0, 5);
        }
    }
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

}
