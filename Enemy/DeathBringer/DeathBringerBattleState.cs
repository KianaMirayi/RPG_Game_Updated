using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBringerBattleState : EnemyState
{
    public Enemy_DeathBringer deathBringer;

    public Transform player;

    private int moveDir;
    public DeathBringerBattleState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_DeathBringer deathBringer) : base(enemyBase, enemyStateMachine, enemyAnimBoolName)
    {
        this.deathBringer = deathBringer;
    }

    public override void Enter()
    {
        base.Enter();

        //player = GameObject.Find("Player").transform;

        player = PlayerManager.instance.player.transform;

        //if (player.GetComponent<PlayerStats>().IsDead)
        //{
        //    enemyStateMachine.changeState(deathBringer.skeletonoveState);
        //}
    }
    public override void Update()
    {
        base.Update();

        if (deathBringer.IsPlayerDetected())
        {
            Debug.Log("find player");
            stateTimer = deathBringer.battleTime;

            if (deathBringer.IsPlayerDetected().distance < deathBringer.attackDistance)  //角色在敌人攻击范围内
            {
                if (canAttack())  // 设置骷髅怪的攻击间隔时间
                {
                    enemyStateMachine.changeState(deathBringer.attackState);

                }
                //else
                //{
                //    enemyStateMachine.changeState(deathBringer.idleState);
                //}

            }
        }
        

        if (player.position.x > deathBringer.transform.position.x)
        {
            moveDir = 1;
        }
        else if (player.position.x < deathBringer.transform.position.x)
        {
            moveDir = -1;
        }

        if (deathBringer.IsPlayerDetected() && deathBringer.IsPlayerDetected().distance < deathBringer.attackDistance - 0.1f)
        {
            return;
        }

        deathBringer.setVelocity(deathBringer.moveSpeed * moveDir, deathBringer.rb.velocity.y);
        Debug.Log(" is in battlestate");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public bool canAttack()
    {
        if (Time.time >= deathBringer.enemyLastAttacked + deathBringer.enemyAttackCoolDown)
        {
            deathBringer.enemyAttackCoolDown = Random.Range(deathBringer.enemyMinAttackCoolDown, deathBringer.enemyMaxAttackCoolDown); //快慢刀
            deathBringer.enemyLastAttacked = Time.time;
            return true;
        }

        Debug.Log("Attack is on cooldown");
        return false;
    }
}
