using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttackState : EnemyState
{
    public Enemy_Archer arcehrEnemy;
    public ArcherAttackState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_Archer arcehrEnemy) : base(enemyBase, enemyStateMachine, enemyAnimBoolName)
    {
        this.arcehrEnemy = arcehrEnemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        arcehrEnemy.enemyLastAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        arcehrEnemy.ZeroVelocity();

        if (Triggercalled)
        {
            enemyStateMachine.changeState(arcehrEnemy.archerBattleState);
        }
    }

}
