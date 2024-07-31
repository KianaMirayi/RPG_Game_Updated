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

        shady.fx.InvokeRepeating("RedBlink", 0, 0.1f);  //��һ��������ʾ���õ�����һ���������ڶ���������ʾ�ӳٶ���ʱ���ٽ��е��ã�������������ʾ�ظ��ʣ����������ظ�һ��

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
