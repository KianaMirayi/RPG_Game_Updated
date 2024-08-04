using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBringerDeadState : EnemyState
{
    public Enemy_DeathBringer deathBringer;
    public DeathBringerDeadState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_DeathBringer deathBringer) : base(enemyBase, enemyStateMachine, enemyAnimBoolName)
    {
        this.deathBringer = deathBringer;
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySfx(65,null);

        deathBringer.anim.SetBool(deathBringer.LastAnimBoolName, true);
        deathBringer.anim.speed = 0;
        deathBringer.cd.enabled = false;

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
            deathBringer.rb.velocity = new Vector2(0, 5);
        }
    }
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

}
