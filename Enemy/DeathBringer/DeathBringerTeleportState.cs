using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBringerTeleportState : EnemyState
{
    public Enemy_DeathBringer deathBringer;
    public DeathBringerTeleportState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_DeathBringer deathBringer) : base(enemyBase, enemyStateMachine, enemyAnimBoolName)
    {
        this.deathBringer = deathBringer;
    }

    public override void Enter()
    {
        base.Enter();

        deathBringer.stats.MakeInvincible(true);

        //deathBringer.FindPosition();

        //Debug.Log("is in teleport state");

        //stateTimer = 1;
    }

    public override void Update()
    {
        base.Update();

        if (Triggercalled)
        {
            if (deathBringer.CanSpellCast())
            {
                Debug.Log("Can Spell");
                enemyStateMachine.changeState(deathBringer.spellCastState);
            }
            else
            {
                enemyStateMachine.changeState(deathBringer.battleState);
            }

            
        }
        
    }

    public override void Exit()
    {
        base.Exit();

        deathBringer.stats.MakeInvincible(false);
    }
}
