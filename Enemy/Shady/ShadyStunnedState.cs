using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadyStunnedState : EnemyState
{
    public Enemy_Shady shady;
    public ShadyStunnedState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_Shady shady) : base(enemyBase, enemyStateMachine, enemyAnimBoolName)
    {
        this.shady = shady;
    }

    public override void Enter()
    {
        base.Enter();

        shady.fx.InvokeRepeating("RedBlink", 0, 0.1f);  //第一个参数表示调用的是哪一个方法，第二个参数表示延迟多少时间再进行调用，第三个参数表示重复率，即多少秒重复一次

        stateTimer = shady.StunnedDuration;

        shady.rb.velocity = new Vector2(-shady.facingDir * shady.StunDir.x, shady.StunDir.y);
    }

    public override void Exit()
    {
        base.Exit();
        shady.fx.Invoke("CancelColorChange", 0);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            enemyStateMachine.changeState(shady.shadyIdleState);
        }
    }
}
