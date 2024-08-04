using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherDeadState : EnemyState
{
    public Enemy_Archer archerEnemy;
    public ArcherDeadState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_Archer archerEnemy) : base(enemyBase, enemyStateMachine, enemyAnimBoolName)
    {
        this.archerEnemy = archerEnemy;
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySfx(59, null);

        archerEnemy.anim.SetBool(archerEnemy.LastAnimBoolName, true);
        archerEnemy.anim.speed = 0;
        archerEnemy.cd.enabled = false;

        stateTimer = 0.1f;

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > 0)
        {
            archerEnemy.rb.velocity = new Vector2(0, 5);
        }
    }
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
