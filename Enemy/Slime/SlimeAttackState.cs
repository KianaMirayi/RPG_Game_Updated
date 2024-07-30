using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttackState : EnemyState
{
    public Enemy_Slime slimeEnemy;
    public SlimeAttackState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_Slime slimeEnemy) : base(enemyBase, enemyStateMachine, enemyAnimBoolName)
    {
        this.slimeEnemy = slimeEnemy;
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Ê·À³Ä·¿ªÊ¼¹¥»÷");
    }

    public override void Exit()
    {
        base.Exit();

        slimeEnemy.enemyLastAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        //slimeEnemy.ZeroVelocity();

        //Debug.Log(slimeEnemy.moveSpeed);

        if (Triggercalled)
        {
            enemyStateMachine.changeState(slimeEnemy.slimeBattleState);
        }
    }

}
