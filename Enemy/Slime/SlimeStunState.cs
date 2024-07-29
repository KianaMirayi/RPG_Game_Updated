using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeStunState : EnemyState
{
    public Enemy_Slime slimeEnemy;
    public SlimeStunState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_Slime slimeEnemy) : base(enemyBase, enemyStateMachine, enemyAnimBoolName)
    {
        this.slimeEnemy = slimeEnemy;
    }

    public override void Enter()
    {
        base.Enter();

        slimeEnemy.fx.InvokeRepeating("RedBlink", 0, 0.1f);  //第一个参数表示调用的是哪一个方法，第二个参数表示延迟多少时间再进行调用，第三个参数表示重复率，即多少秒重复一次

        stateTimer = slimeEnemy.StunnedDuration;

        slimeEnemy.rb.velocity = new Vector2(-slimeEnemy.facingDir * slimeEnemy.StunDir.x, slimeEnemy.StunDir.y);
    }

    public override void Exit()
    {
        base.Exit();
        slimeEnemy.fx.Invoke("CancelColorChange", 0);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            enemyStateMachine.changeState(slimeEnemy.slimeIdleState);
        }
    }
}
