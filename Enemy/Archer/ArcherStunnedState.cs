using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherStunnedState : EnemyState
{
    public Enemy_Archer archerEnemy;
    public ArcherStunnedState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_Archer archerEnemy) : base(enemyBase, enemyStateMachine, enemyAnimBoolName)
    {
        this.archerEnemy = archerEnemy;
    }

    public override void Enter()
    {
        base.Enter();

        archerEnemy.fx.InvokeRepeating("RedBlink", 0, 0.1f);  //��һ��������ʾ���õ�����һ���������ڶ���������ʾ�ӳٶ���ʱ���ٽ��е��ã�������������ʾ�ظ��ʣ����������ظ�һ��

        stateTimer = archerEnemy.StunnedDuration;

        archerEnemy.rb.velocity = new Vector2(-archerEnemy.facingDir * archerEnemy.StunDir.x, archerEnemy.StunDir.y);
    }

    public override void Exit()
    {
        base.Exit();
        archerEnemy.fx.Invoke("CancelColorChange", 0);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            enemyStateMachine.changeState(archerEnemy.archerIdleState);
        }
    }
}
