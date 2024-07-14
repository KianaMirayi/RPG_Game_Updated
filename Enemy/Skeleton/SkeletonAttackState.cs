using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonAttackState : EnemyState
{
    public Enemy_Skeleton skeletonEnemy;
    public SkeletonAttackState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_Skeleton enemy) : base(enemyBase, enemyStateMachine, enemyAnimBoolName)
    {
        skeletonEnemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        skeletonEnemy.enemyLastAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        skeletonEnemy.ZeroVelocity();

        if (Triggercalled)
        {
            enemyStateMachine.changeState(skeletonEnemy.skeletonBattleState);
        }
    }

    
}
