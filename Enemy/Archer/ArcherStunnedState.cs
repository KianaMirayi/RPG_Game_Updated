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

        archerEnemy.fx.InvokeRepeating("RedBlink", 0, 0.1f);  //第一个参数表示调用的是哪一个方法，第二个参数表示延迟多少时间再进行调用，第三个参数表示重复率，即多少秒重复一次

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
