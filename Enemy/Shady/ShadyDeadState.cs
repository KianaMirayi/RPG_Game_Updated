using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadyDeadState : EnemyState
{
    public Enemy_Shady shady;

    public ShadyDeadState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_Shady shady) : base(enemyBase, enemyStateMachine, enemyAnimBoolName)
    {
        this.shady = shady;
    }

    public override void Enter()
    {
        base.Enter();

        //shady.anim.SetBool(shady.LastAnimBoolName, true);
        //shady.anim.speed = 0;
        ////shady.cd.enabled = false;

        //stateTimer = 0.1f;

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        //if (stateTimer > 0)
        //{
        //    shady.rb.velocity = new Vector2(0, 5);
        //}

        if (Triggercalled)
        { 
            shady.ShadySelfDestory();
        }
    }
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
