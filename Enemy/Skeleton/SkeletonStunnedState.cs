using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStunnedState : EnemyState
{
    Enemy_Skeleton skeletonEnemy;
    public SkeletonStunnedState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_Skeleton enemy) : base(enemyBase, enemyStateMachine, enemyAnimBoolName)
    {
        skeletonEnemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        skeletonEnemy.fx.InvokeRepeating("RedBlink", 0, 0.1f);  //第一个参数表示调用的是哪一个方法，第二个参数表示延迟多少时间再进行调用，第三个参数表示重复率，即多少秒重复一次

        stateTimer = skeletonEnemy.StunnedDuration;

        skeletonEnemy.rb.velocity = new Vector2(-skeletonEnemy.facingDir * skeletonEnemy.StunDir.x, skeletonEnemy.StunDir.y);
    }

    public override void Exit()
    {
        base.Exit();
        skeletonEnemy.fx.Invoke("CancelRedBlink", 0);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            enemyStateMachine.changeState(skeletonEnemy.skeIdleState);
        }
    }
}
