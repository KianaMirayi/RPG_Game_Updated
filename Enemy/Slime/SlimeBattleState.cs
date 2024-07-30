using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeBattleState : EnemyState
{
    public Enemy_Slime slimeEnemy;

    public Transform player;

    public int moveDir;

    public SlimeBattleState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string enemyAnimBoolName, Enemy_Slime slimeEnemy) : base(enemyBase, enemyStateMachine, enemyAnimBoolName)
    {
        this.slimeEnemy = slimeEnemy;
    }

    public override void Enter()
    {
        base.Enter();

        //player = GameObject.Find("Player").transform;

        player = PlayerManager.instance.player.transform;

        if (player.GetComponent<PlayerStats>().IsDead)
        {
            enemyStateMachine.changeState(slimeEnemy.slimeMoveState);
        }

        Debug.Log("史莱姆进入战斗状态");
    }
    public override void Update()
    {
        base.Update();

        if (slimeEnemy.IsPlayerDetected())
        {
            stateTimer = slimeEnemy.battleTime;

            if (slimeEnemy.IsPlayerDetected().distance < slimeEnemy.attackDistance)  //角色在敌人攻击范围内
            {
                if (canAttack())  // 设置骷髅怪的攻击间隔时间
                {
                    enemyStateMachine.changeState(slimeEnemy.slimeAttackState);

                }

            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.position, slimeEnemy.transform.position) > 7)  // 战斗状态计时器小于0或者角色与敌人距离过远时进入IdleState
            {
                enemyStateMachine.changeState(slimeEnemy.slimeIdleState);
            }

        }

        if (player.position.x > slimeEnemy.transform.position.x)
        {
            moveDir = 1;
        }
        else if (player.position.x < slimeEnemy.transform.position.x)
        {
            moveDir = -1;
        }

        if (slimeEnemy.IsPlayerDetected() && slimeEnemy.IsPlayerDetected().distance < slimeEnemy.attackDistance - 0.5f)
        { 
            return;
        }

        slimeEnemy.setVelocity(slimeEnemy.moveSpeed * moveDir, slimeEnemy.rb.velocity.y);
        Debug.Log("Slime is in battlestate");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public bool canAttack()
    {
        if (Time.time >= slimeEnemy.enemyLastAttacked + slimeEnemy.enemyAttackCoolDown)
        {
            slimeEnemy.enemyAttackCoolDown = Random.Range(slimeEnemy.enemyMinAttackCoolDown, slimeEnemy.enemyMaxAttackCoolDown); //快慢刀
            slimeEnemy.enemyLastAttacked = Time.time;
            return true;
        }

        Debug.Log("Attack is on cooldown");
        return false;
    }

}
