using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBringerIdleState : EnemyState
{
    public Enemy_DeathBringer deathBringer;

    public Transform player;
    public DeathBringerIdleState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_DeathBringer deathBringer) : base(enemyBase, enemyStateMachine, enemyAnimBoolName)
    {
        this.deathBringer = deathBringer;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = deathBringer.idleTime; ;

        //Debug.Log("Entered Idle State");
    }


    public override void Update()
    {
        base.Update();
        //Debug.Log("Skeleton is in idle");

        //if (Vector2.Distance(player.position, deathBringer.transform.position) < 7)
        //{
        //    deathBringer.bossFightBegun = true;
        //}


        //boss在发现玩家后必定会进行一次传送
        if (Input.GetKeyDown(KeyCode.B) || deathBringer.IsPlayerDetected()) //因为boss发现玩家后不会重新回到idle，所以它会一直在战斗状态中
        {
            enemyStateMachine.changeState(deathBringer.teleportState);
        }

        //if (stateTimer < 0 && deathBringer.bossFightBegun)
        //{
        //    enemyStateMachine.changeState(deathBringer.battleState);
        //}


    }
    public override void Exit()
    {
        base.Exit();

        //AudioManager.instance.PlaySfx(19, skeletonEnemy.transform); //For Test
        //Debug.Log("Exiting Idle State");
    }
}
