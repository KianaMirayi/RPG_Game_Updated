using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherGroundedState : EnemyState
{
    public Enemy_Archer archerEnemy;

    public Transform player;

    public ArcherGroundedState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_Archer archerEnemy) : base(enemyBase, enemyStateMachine, enemyAnimBoolName)
    {
        this.archerEnemy = archerEnemy;
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

        if (archerEnemy.IsPlayerDetected() || Vector2.Distance(archerEnemy.transform.position, player.position) < archerEnemy.agreDistacne)  // ͬ����Զ�����⵽���ʱ���Լ������̾����⵽���ʱ����ս��״̬
        {

            enemyStateMachine.changeState(archerEnemy.archerBattleState);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
