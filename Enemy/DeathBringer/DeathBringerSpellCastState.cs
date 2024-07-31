using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBringerSpellCastState : EnemyState
{
    public Enemy_DeathBringer deathBringer;

    private int spellCastAmount;
    //[SerializeField]private float spellCoolDown;
    private float spellTimer;

    public DeathBringerSpellCastState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_DeathBringer deathBringer) : base(enemyBase, enemyStateMachine, enemyAnimBoolName)
    {
        this.deathBringer = deathBringer;
    }

    public override void Enter()
    {
        base.Enter();
        spellCastAmount = deathBringer.spellCastAmount;
        spellTimer = 0f;
        Debug.Log("is in spell cast");
    }

    public override void Update()
    {
        base.Update();

        spellTimer -= Time.deltaTime;
        //Debug.Log(spellTimer);

        if (CanCast())
        {
            deathBringer.CastSpell();
            Debug.Log("Cast Spelled");
        }
        //else
        //{ 
        //    enemyStateMachine.changeState(deathBringer.teleportState);
            
        //}

        if (spellCastAmount <= 0)
        {
            enemyStateMachine.changeState(deathBringer.teleportState);
        }
    }


    public override void Exit()
    {
        base.Exit();
        deathBringer.lastTimepellCast = Time.time;


    }
    private bool CanCast()
    {
        if (spellCastAmount > 0 && spellTimer <= 0)
        {
            spellCastAmount--;
            spellTimer = deathBringer.spellCoolDown;
            return true;
        }

        return false;
    }
}
