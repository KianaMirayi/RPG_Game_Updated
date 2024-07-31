using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBringerAttackState : EnemyState
{
    public Enemy_DeathBringer deathBringer;

    public DeathBringerAttackState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_DeathBringer deathBringer) : base(enemyBase, enemyStateMachine, enemyAnimBoolName)
    {
        this.deathBringer = deathBringer;
    }

    public override void Enter()
    {
        base.Enter();

        deathBringer.chanceToTeleport += 5; //boss��ÿ�ι����������ӿɴ��͵ĸ���
    }

    public override void Exit()
    {
        base.Exit();

        deathBringer.enemyLastAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        deathBringer.ZeroVelocity();

        if (Triggercalled)
        {
            if (deathBringer.CanTeleport()) //boss���ܴ���ʱ�Դ���ս��״̬
            {
                enemyStateMachine.changeState(deathBringer.teleportState);
            }
            else
            {
                enemyStateMachine.changeState(deathBringer.battleState);
            }

            
        }
    }

}
