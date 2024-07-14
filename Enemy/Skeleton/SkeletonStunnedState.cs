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

        skeletonEnemy.fx.InvokeRepeating("RedBlink", 0, 0.1f);  //��һ��������ʾ���õ�����һ���������ڶ���������ʾ�ӳٶ���ʱ���ٽ��е��ã�������������ʾ�ظ��ʣ����������ظ�һ��

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
