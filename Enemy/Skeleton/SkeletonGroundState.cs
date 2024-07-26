using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundState : EnemyState
{
    public Enemy_Skeleton skeletonEnemy;

    public Transform player;
    public SkeletonGroundState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_Skeleton enemy) : base(enemyBase, enemyStateMachine, enemyAnimBoolName)
    {
        skeletonEnemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        //player = GameObject.Find("Player").transform;

        player = PlayerManager.instance.player.transform;
    }

    public override void Update()
    {
        base.Update();

        if (skeletonEnemy.IsPlayerDetected() || Vector2.Distance(skeletonEnemy.transform.position, player.position) < 2)  // ͬ����Զ�����⵽���ʱ���Լ������̾����⵽���ʱ����ս��״̬
        {

            enemyStateMachine.changeState(skeletonEnemy.skeletonBattleState);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }

}
