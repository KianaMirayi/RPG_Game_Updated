using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDeadState : EnemyState
{
    public Enemy_Slime slimeEnemy;
    public SlimeDeadState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_Slime slimeEnemy) : base(enemyBase, enemyStateMachine, enemyAnimBoolName)
    {
        this.slimeEnemy = slimeEnemy;
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySfx(52,null);

        slimeEnemy.anim.SetBool(slimeEnemy.LastAnimBoolName, true);
        slimeEnemy.anim.speed = 0;
        slimeEnemy.cd.enabled = false;

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
            slimeEnemy.rb.velocity = new Vector2(0, 5);
        }
    }
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
